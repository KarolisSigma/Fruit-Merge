using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private FruitInfo fruitInfo;
    private int fruitcount;
    void Awake()
    {
        fruitcount=FruitManager.instance.fruitInfos.Count;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Fruit>()==null) return;
        FruitInfo otherInfo=collision.gameObject.GetComponent<Fruit>().fruitInfo;
        if(otherInfo==null) return;
        if (otherInfo.fruitIndex == fruitInfo.fruitIndex && fruitInfo.fruitIndex<fruitcount-1){
            ReplaceFruit(collision.gameObject);
        }
    }

    void ReplaceFruit(GameObject other){
        Vector3 newPos = (other.transform.position+transform.position)*0.5f;
        Destroy(other.gameObject);
        transform.position = newPos;
        SetInfo(FruitManager.instance.fruitInfos[fruitInfo.fruitIndex+1]);
    }

    public void SetInfo(FruitInfo info){
        fruitInfo = info;
        GetComponent<SpriteRenderer>().color = fruitInfo.color;
        transform.localScale = fruitInfo.size;
        ScoreManager.instance.UpdateScore(info.scoreAmount);
    }
}

[Serializable]
public class FruitInfo{
    public Vector2 size;
    public Color color;
    public int fruitIndex;
    public int scoreAmount;
}