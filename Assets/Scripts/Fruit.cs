using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(!collision.gameObject.CompareTag("Fruit")) return;
        //if(collision.gameObject.GetComponent<Fruit>()==null) return;
        int index =collision.gameObject.GetComponent<Fruit>().fruitInfo.fruitIndex;
        if (index == fruitInfo.fruitIndex && fruitInfo.fruitIndex<fruitcount-1){
            ReplaceFruit(collision.gameObject);
        }
    }
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Border"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }*/

    void ReplaceFruit(GameObject other){
        Vector3 newPos = (other.transform.position+transform.position)*0.5f;
        Destroy(other.gameObject);
        transform.position = newPos;
        SetInfo(FruitManager.instance.fruitInfos[fruitInfo.fruitIndex+1]);
    }

    public void SetInfo(FruitInfo info){
        fruitInfo = info;

        GetComponent<SpriteRenderer>().sprite = fruitInfo.sprite;

        CircleCollider2D circleCollider2D= GetComponent<CircleCollider2D>();
        circleCollider2D.offset = fruitInfo.circleColliderOffset;
        circleCollider2D.radius=fruitInfo.radius;

        transform.localScale = fruitInfo.size;

        ScoreManager.instance.UpdateScore(info.scoreAmount);
    }
}

[Serializable]
public class FruitInfo{
    public Vector2 size;
    public Sprite sprite;
    public Vector2 circleColliderOffset;
    public float radius;
    public int fruitIndex;
    public int scoreAmount;
}