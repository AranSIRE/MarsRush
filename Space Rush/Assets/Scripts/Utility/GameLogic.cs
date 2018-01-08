using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    // Storage of Global Variables;
    
    private GameState gamestate = GameState.TitleScreen;
    internal GameState Gamestate
    {
        get
        {
            return gamestate;
        }

        set
        {
            gamestate = value;
        }
    }

    private TitleScreen title;
    private ObstacleGenerator obstacle;
    private ObstacleGenerator obstacle2;
    private Health health;
    private AudioManager audioManager;
    private bool EnableRestart = false;
    [SerializeField] private int level=0;

    [SerializeField] private GameObject PlayerPrefab;


    private void Update()
    {
        if (Gamestate == GameState.TitleScreen)
        {
            if (Input.GetMouseButton(0))
            {
               StartGame();
            }
        }

        if (Gamestate == GameState.GameOver && EnableRestart == true)
        {
            if (Input.GetMouseButton(0))
            {
                StartGame();
                EnableRestart = false;
            }
        }
    }


    private void Start()
    {
        title = GameObject.Find("Title").GetComponent<TitleScreen>();
        obstacle = GameObject.Find("ObstacleGenerator").GetComponent<ObstacleGenerator>();
        obstacle2 = GameObject.Find("ObstacleGenerator2").GetComponent<ObstacleGenerator>();
        audioManager = GetComponent<AudioManager>();

    }
    
    public void GameOver()
    {
        Gamestate = GameState.GameOver;
        title.GameOverScreen();
        level = 0;
    }


    public void StartGame()
    {       
       GameObject.Find("Title").GetComponent<TitleScreen>().DisplayNextLevel();
       gamestate = GameState.Ingame;      
       GameObject PlayerShip = Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
       PlayerShip.name = "PlayerShip";
       PlayerShip.gameObject.SetActive(true);
       health = GameObject.Find("HealthBar").GetComponent<Health>();
       health.ShowAll();
       title.HideElements();
       obstacle.StartGame();
       obstacle2.SetBehind();
       obstacle2.StartGame();             
      
    }

    public void NextLevel()
    {
        level = level +1;
        GameObject.Find("Title").GetComponent<TitleScreen>().DisplayNextLevel();

        if (level > 3)
        {
            obstacle2.ActivateGenerator();
        }

    }


    public void enableRestart()
    {
        EnableRestart = true;
    }


    public void PlaySound(string soundName)
    {
        audioManager.Play(soundName);
    }

    
}
