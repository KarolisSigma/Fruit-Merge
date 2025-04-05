using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image nextFruit;
    private Vector2 clampDropperPos;
    public ParticleSystem click;
    public ParticleSystem mergeEffect;
    public bool isPlaying;

    public void MergeEffect(Vector3 pos, float diameter, Color color, Color color2){
        mergeEffect.transform.position = pos;
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.startSize = diameter;
        emitParams.startColor = color;
        mergeEffect.Emit(emitParams, 3);
        emitParams.startColor = color2;
        emitParams.rotation = 30;
        mergeEffect.Emit(emitParams, 3);
    }
    void Awake()
    {
        instance=this;
        cam = Camera.main;
        dropperYPos = dropper.transform.position.y;
        currentFruit = fruitInfos[Random.Range(0, 7)];
        incomingFruit = fruitInfos[Random.Range(0, 7)];
        StartCoroutine(drop());
    }


    IEnumerator drop(){
        yield return new WaitForSeconds(0.5f);
        dropper.SetActive(true);
        currentFruit=incomingFruit;
        incomingFruit = fruitInfos[Random.Range(0, 7)];

        dropper.GetComponent<SpriteRenderer>().sprite=currentFruit.sprite;
        dropper.transform.localScale=Vector2.one*currentFruit.diameter;
        nextFruit.sprite = incomingFruit.sprite;

        clampDropperPos = new Vector2(-2.8f+currentFruit.diameter/2f, 2.8f-currentFruit.diameter/2);

        bool dropped = false;
        bool registerClick=true;
        while(!dropped){

            if(!isPlaying){
                registerClick=false;
                yield return null;
                continue;
            }
            if(!registerClick) yield return null;

            Vector2 pos=Vector2.zero;
            pos.x = Mathf.Clamp(cam.ScreenToWorldPoint(Input.mousePosition).x, clampDropperPos.x, clampDropperPos.y);
            pos.y = dropperYPos;
            dropper.transform.position = pos;
            
            if(Input.GetKeyUp(KeyCode.Mouse0)){
                Vector2 clickpos = cam.ScreenToWorldPoint(Input.mousePosition);
                click.transform.position = clickpos;
                click.Emit(1);
                AudioManager.instance.PlayPop();
                Spawn(dropper.transform.position);
                dropped=true;
            }
            registerClick=true;
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
