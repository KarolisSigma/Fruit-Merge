using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FruitManager : MonoBehaviour
{
    public List<FruitInfo> fruitInfos= new List<FruitInfo>();
    public static FruitManager instance;
    public GameObject fruitPrefab;
    public GameObject dropper;
    private float dropperYPos;
    private Camera cam;
    private FruitInfo incomingFruit;
    private FruitInfo currentFruit;
    void Awake()
    {
        instance=this;
        cam = Camera.main;
        dropperYPos = dropper.transform.position.y;
        currentFruit = fruitInfos[Random.Range(0, fruitInfos.Count-4)];
        incomingFruit = fruitInfos[Random.Range(0, fruitInfos.Count-4)];
        StartCoroutine(drop());
    }


    IEnumerator drop(){
        currentFruit=incomingFruit;
        dropper.GetComponent<SpriteRenderer>().color=currentFruit.color;
        dropper.transform.localScale=currentFruit.size;
        incomingFruit = fruitInfos[Random.Range(0, fruitInfos.Count-4)];
        bool dropped = false;
        yield return new WaitForSeconds(0.2f);
        while(!dropped){
            if(Input.GetKey(KeyCode.Mouse0)){
                Vector2 mousePos = Input.mousePosition;
                dropper.transform.position = new Vector3(cam.ScreenToWorldPoint(mousePos).x, dropperYPos, 0);
            }
            if(Input.GetKeyUp(KeyCode.Mouse0)){
                Spawn(dropper.transform.position);
                dropped=true;
            }
            yield return null;
        }
    }

    public void Spawn(Vector3 spawnPos){
        GameObject fruit = Instantiate(fruitPrefab, spawnPos, Quaternion.identity);
        fruit.GetComponent<Fruit>().SetInfo(currentFruit);
        StartCoroutine(drop());
    }
}
