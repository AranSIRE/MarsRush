using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameScore : MonoBehaviour {

    [SerializeField]
    private Text text;
    private int score = 0;
    private int[] HighScores;
    
    void Start()
    {
        addScore(0);
    }

   
    public void CashInScores(int score)
    {      
        StartCoroutine("CashIn",score);
    }

    IEnumerator CashIn(int points)
    {
        for (int i = 0; i < points; i++)
        {
            addScore(1);
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    public void addScore(int points)
    {
        score =  score + points;
        text.text = "Score:" + score.ToString() + "$";
    } 
    
    public int getScore()
    {
        return score;
    }

    public void SetScore(int x)
    {
        score = x;
        text.text = "Score:" + score.ToString() + "$";
    }





    //ToD0 top 10 Ranking Score Evaluator

}
