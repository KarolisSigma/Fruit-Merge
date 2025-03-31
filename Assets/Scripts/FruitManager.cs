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
    public GameObject nextFruit;
    private Vector2 clampDropperPos;
    void Awake()
    {
        instance=this;
        cam = Camera.main;
        dropperYPos = dropper.transform.position.y;
        currentFruit = fruitInfos[Random.Range(0, fruitInfos.Count-4)];
        incomingFruit = fruitInfos[Random.Range(0, fruitInfos.Count-4)];
        //nextFruit.GetComponent<SpriteRenderer>().color = incomingFruit.color;
        nextFruit.transform.localScale = incomingFruit.size;
        StartCoroutine(drop());
    }


    IEnumerator drop(){
        yield return new WaitForSeconds(0.2f);
        dropper.SetActive(true);
        currentFruit=incomingFruit;
        incomingFruit = fruitInfos[Random.Range(0, fruitInfos.Count-4)];

        dropper.GetComponent<SpriteRenderer>().sprite=currentFruit.sprite;
        dropper.transform.localScale=currentFruit.size;
        nextFruit.GetComponent<SpriteRenderer>().sprite = incomingFruit.sprite;

        clampDropperPos = new Vector2(-2.8f+currentFruit.size.x/2f, 2.8f-currentFruit.size.x/2);
        //nextFruit.transform.localScale = incomingFruit.size;
        bool dropped = false;
        while(!dropped){
            Vector2 pos=Vector2.zero;
            pos.x = Mathf.Clamp(cam.ScreenToWorldPoint(Input.mousePosition).x, clampDropperPos.x, clampDropperPos.y);
            pos.y = dropperYPos;
            dropper.transform.position = pos;
            
            if(Input.GetKeyUp(KeyCode.Mouse0)){
                Spawn(dropper.transform.position);
                dropped=true;
            }
            yield return null;
        }
        dropper.SetActive(false);
    }

    public void Spawn(Vector3 spawnPos){
        GameObject fruit = Instantiate(fruitPrefab, spawnPos, Quaternion.identity);
        fruit.GetComponent<Fruit>().SetInfo(currentFruit);
        StartCoroutine(drop());
    }
}
