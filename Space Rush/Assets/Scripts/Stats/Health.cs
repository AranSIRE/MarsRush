using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    // Health Stats:    
    private int Hp = 5;
    private bool invulnerable = false;
    private bool death;
    GameLogic gamelogic;
   
    // HealthBar
    [SerializeField] Sprite healthIcon;
    [SerializeField] Image[] healthBar;
    [SerializeField] GameObject healthPanel;

   


    // Use this for initialization
    void Start()
    {       
        gamelogic = GameObject.Find("Gamelogic").GetComponent<GameLogic>();       
        CreateHealthBar();
        HideAll();
        
    }


    public void Hit()
    {
       
        if (gamelogic.Gamestate!= GameState.GameOver){

            if (invulnerable == false)
            {
                Hp = Hp - 1;
                HideStar();
                SoundEffect();
                if (Hp != 0)
                {
                    StartCoroutine("InvulnerablilityFrames");
                }
                else
                {
                    GameObject.Find("PlayerShip").GetComponent<Controls>().Die();
                    GameOver();
                    Hp = 5;
                }
            }
        }            
    }

    private void CreateHealthBar()
    {
        for(int i = 0; i < Hp; i++)
        {            
            GameObject NewObj = new GameObject(); 
            Image NewImage = NewObj.AddComponent<Image>(); 
            NewImage.sprite = healthIcon;
            NewObj.GetComponent<RectTransform>().SetParent(healthPanel.transform); 
            NewObj.SetActive(true);
            NewObj.GetComponent<RectTransform>().sizeDelta = new Vector2(60,60);
            healthBar[i] = NewObj.GetComponent<Image>();            
        }
    }


    private void GameOver()
    {      
        gamelogic.GameOver();        
    }

    private void SoundEffect()
    {
        gamelogic.PlaySound("Hit");
       
    }

    IEnumerator InvulnerablilityFrames()
    {
        invulnerable = true;
        GameObject.Find("PlayerShip").GetComponent<Controls>().Flash();
        yield return new WaitForSeconds(1);
        invulnerable = false;
    }

    private void HideStar()
    {
        healthBar[Hp].enabled = false;
    }

    private void HideAll()
    {
        for (int i=0; i < healthBar.Length; i++)
        {
            healthBar[i].enabled = false;
        }
     
    }
    public void ShowAll()
    {
        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].enabled = true;
        }
    }

    

    


    
     
}
