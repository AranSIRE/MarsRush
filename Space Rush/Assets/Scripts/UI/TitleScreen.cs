using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {
    
    [SerializeField] private Text blinkingText;
    [SerializeField] private Text score;
    [SerializeField] private Image title;
    [SerializeField] private Image gameover;


    private GameLogic gamelogic;
    private int level = 1;
    bool GameBegin = false;


    // Use this for initialization
    void Start()
    {
        StartCoroutine("Blink");
        score.enabled = false;
        gameover.enabled = false;
        gamelogic = GameObject.Find("Gamelogic").GetComponent<GameLogic>();

    }

    IEnumerator Blink()
    {
        while (GameBegin == false)
        {
            yield return new WaitForSeconds(1);
            blinkingText.enabled = false;
            yield return new WaitForSeconds(0.5f);
            blinkingText.enabled = true;
        }
        blinkingText.enabled = false;

    }

    public void DisplayNextLevel()
    {
       StartCoroutine("NextLevel");
    }

    IEnumerator NextLevel()
    {        
        blinkingText.text = "Level "+ level;
        blinkingText.enabled = true;       
        yield return new WaitForSeconds(5);  
        level++;
        if (GameObject.Find("Gamelogic").GetComponent<GameLogic>().Gamestate != GameState.GameOver)
        {
            blinkingText.enabled = false;
        }

    }
    
    public void HideElements()
    {
        GameBegin = true;
        blinkingText.enabled = false;
        title.enabled = false;
        score.enabled = true;
        gameover.enabled = false;
        StartCoroutine("NextLevel");
    }

    public void GameOverScreen()
    {
        StartCoroutine("CashInFinalScore");
        level = 1;
    }

    IEnumerator CashInFinalScore()
    {
        score.enabled = false;
        yield return new WaitForSeconds(0.8f);
        int points = score.GetComponent<GameScore>().getScore();
        blinkingText.enabled = true;       
        gameover.enabled = true;
        blinkingText.text = "";

        yield return new WaitForSeconds(0.4f);
        blinkingText.text = "FinalScore: $";

        GameObject.Find("Gamelogic").GetComponent<AudioManager>().Play("MoneyCounter");
        for (int i = 0; i < points; i++)
        {
            if ((points-i-10) > 10){i = i + 10;}
            if ((points-i-100) >  100){i = i + 100;}
            blinkingText.text = "FinalScore: " + i + "$";
            yield return new WaitForSeconds(0.0001f);
           
        }
        gamelogic.GetComponent<AudioManager>().Stop("MoneyCounter");
        yield return new WaitForSeconds(0.2f);
        gamelogic.GetComponent<AudioManager>().Stop("CashIn");
        yield return new WaitForSeconds(0.2f);
        AddComment(points);
        score.GetComponent<GameScore>().SetScore(0);
        gamelogic.enableRestart();
        

    }

    //  A little Humor feature, For player scores.
    public void AddComment(int points)
    {
        if (points == 0)
        {
            blinkingText.text = "FinalScore: " + points + "$" + '\n'
            + "Just Try harder, Earthfoot.";
        }
        if (points < 1000 && points != 0)
        {
            blinkingText.text = "FinalScore: " + points + "$" + '\n'
            + "Better luck next time.";
        }
        if (points > 1000)
        {
            blinkingText.text = "FinalScore: " + points + "$" + '\n'
            + "You ain't good, but ya ain't bad.";
        }
        if (points > 5000)
        {
            blinkingText.text = "FinalScore: " + points + "$" + '\n'
            + "You 're on a High Roll mate.";
        }
        if (points > 10000)
        {
            blinkingText.text = "FinalScore: " + points + "$" + '\n'
            + "Holy, Cow You flew over the moon!";
        }
    }



}