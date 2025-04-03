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
        int index =collision.gameObject.GetComponent<Fruit>().fruitInfo.fruitIndex;
        if (index == fruitInfo.fruitIndex && fruitInfo.fruitIndex<fruitcount-1){
            ReplaceFruit(collision.gameObject);
        }
    }

    void ReplaceFruit(GameObject other){
        AudioManager.instance.PlayMerge();

        ScoreManager.instance.UpdateScore(fruitInfo.scoreAmount);

        Vector3 newPos = (other.transform.position+transform.position)*0.5f;
        Destroy(other.gameObject);
        transform.position = newPos;
        SetInfo(FruitManager.instance.fruitInfos[fruitInfo.fruitIndex+1]);
    }

    public void SetInfo(FruitInfo info){
        fruitInfo = info;

        GetComponent<SpriteRenderer>().sprite = fruitInfo.sprite;
        
        GetComponent<Rigidbody2D>().mass = Mathf.Pow(info.diameter/2f, 2)*Mathf.PI;
        
        CircleCollider2D circleCollider2D= GetComponent<CircleCollider2D>();
        circleCollider2D.offset = fruitInfo.circleColliderOffset;
        circleCollider2D.radius=fruitInfo.colliderRadius;

        transform.localScale = Vector2.one*info.diameter;
    }
}

[Serializable]
public class FruitInfo{
    public float diameter;
    public Sprite sprite;
    public Vector2 circleColliderOffset;
    public float colliderRadius;
    public int fruitIndex;
    public int scoreAmount;
}