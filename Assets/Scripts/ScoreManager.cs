using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public int score;

    void Awake()
    {
        if (instance == null) instance=this;
    }

    public void UpdateScore(int amount){
        score+=amount;
        scoreText.text = score.ToString();
    }
}
