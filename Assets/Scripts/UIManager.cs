using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject restartPanel;
    public TextMeshProUGUI restartText;
    public static UIManager instance;
    public GameObject playMenu;
    public GameObject exitBtn;

    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        FruitManager.instance.isPlaying = false;
        playMenu.SetActive(true);
    }

    public void StartGame(){
        FruitManager.instance.isPlaying = true;
        playMenu.SetActive(false);
        exitBtn.SetActive(true);
    }

    void Pause(bool pause){
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        FruitManager.instance.isPlaying=!pause;
        foreach (GameObject fruit in fruits){
            fruit.GetComponent<Rigidbody2D>().simulated=!pause;
        }
    }

    public void GameOver(bool over){
        Pause(over);
        exitBtn.SetActive(!over);
        restartPanel.SetActive(over);
        if(!over){
            ScoreManager.instance.score=0;
            ScoreManager.instance.UpdateScore(0);
            DeleteFruits();
            return;
        }

        int score = ScoreManager.instance.score;
        int highscore=PlayerPrefs.GetInt("Highscore");
        if(score>highscore){
            PlayerPrefs.SetInt("Highscore", score);
            PlayerPrefs.Save();
            highscore=score;
        }
        restartText.text = $"Score: {score}\nHighScore: {highscore}";
    }

    void DeleteFruits(){
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (GameObject fruit in fruits){
            Destroy(fruit);
        }
    }
}
