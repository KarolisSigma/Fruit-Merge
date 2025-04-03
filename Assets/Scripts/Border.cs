using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private List<ContactInfo> contactInfos= new List<ContactInfo>();
    public float timeInContact;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fruit"){
            ContactInfo info = new(){
                ID = collision.gameObject.GetInstanceID(),
                fruit = collision.gameObject,
                coroutine = StartCoroutine(Waiter(collision.gameObject))
            };

            contactInfos.Add(info);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        int exitId = collision.gameObject.GetInstanceID();
        List<ContactInfo> infoToRemove = new();

        foreach(ContactInfo info in contactInfos){
            if(info.ID == exitId){
                StopCoroutine(info.coroutine);
                infoToRemove.Add(info);
            }
        }

        foreach(ContactInfo info in infoToRemove){
            contactInfos.Remove(info);
        }
    }
    
    IEnumerator Waiter(GameObject fruit){
        yield return new WaitForSeconds(timeInContact);
        if(fruit!=null){
            print("End game");
        }
    }

}

[Serializable]
public class ContactInfo{
    public int ID;
    public GameObject fruit;
    public Coroutine coroutine;
}