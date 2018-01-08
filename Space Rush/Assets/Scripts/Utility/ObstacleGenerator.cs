using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ObstacleGenerator : MonoBehaviour {

    // GameLogic: 
    public GameLogic gamelogic;
    public GameObject[] Hazards;     
    public GameObject[] Bosses;
    Transform BehindScene;
    [SerializeField] private bool Behind;
    [SerializeField] private int ObjectsDestroyed =0;
    [SerializeField] private int Objective = 1;


    // private GameState gameState;

    //:Level
    [SerializeField] private int numberOfObstacles = 1;
    [SerializeField] private float SpeedOfObstacles = 1f;   
    [SerializeField] private float SpawnRate = 1f;
    private int difficulty = 1;

    public void SetBehind()
    {
        Behind = true;      
    }

    public void ActivateGenerator()
    {
        Behind = false;
        StartCoroutine("SpawnHazards");
    }

    private void IncreaseDificulty()
    {
        numberOfObstacles = numberOfObstacles + 1;
        SpeedOfObstacles = SpeedOfObstacles + 0.5f;
        difficulty++;
        gamelogic.NextLevel();
        ObjectsDestroyed = 0;
        Objective = Objective + 5;

    }

    private void ResetVariables()
    {
        numberOfObstacles = 1;
        SpeedOfObstacles = 1f;      
        SpawnRate = 1;
        difficulty = 1;
        Objective = 1;
    }


    public void StartGame()
    {
        gamelogic = FindObjectOfType<GameLogic>();
        //gameState = gamelogic.Gamestate;
        ResetVariables();
        StartCoroutine("SpawnHazards");
        GameObject.Find("BehindScene");
    }    	
    
    
    public void ObjectiveDestroyed()
    {
        ObjectsDestroyed= ObjectsDestroyed+1;
    }


    IEnumerator SpawnHazards()
    {

        // This Spawn Generator Spawns objects behind so we 
        // intruduce this mechanic a little bit later In the Game.
       
       
        while(gamelogic.Gamestate != GameState.GameOver  &&  Behind == false)
        {
                int ObstaclesRemaining = numberOfObstacles;
                while (gamelogic.Gamestate == GameState.Ingame && ObstaclesRemaining > 0)
                {              
                    System.Random rng = new System.Random();
                    int spawn = rng.Next(0,Hazards.Length);
                    float RandomHeight = rng.Next(-7, 7);    
                    Vector3 randomLocation = new Vector3(transform.localPosition.x,RandomHeight, 0);
                    GameObject newObstacle = Instantiate(Hazards[spawn], randomLocation, Quaternion.identity) as GameObject;

                    if (newObstacle.GetComponent<AI_EnemyFighter>()!=null && difficulty == 1)
                    {
                        newObstacle.GetComponent<AI_EnemyFighter>().ResetDificulty();
                    }
                    if (newObstacle.GetComponent<AI_EnemyFighter>()!=null && difficulty > 1)
                    {
                        newObstacle.GetComponent<AI_EnemyFighter>().IncreaseStats(difficulty);
                    }
                    ObstaclesRemaining--;

                    if (ObjectsDestroyed >= Objective)
                    {
                        IncreaseDificulty();
                        yield return new WaitForSeconds(5);
                    }
                    yield return new WaitForSeconds(SpawnRate);

                    
                } 
        }
    }  



}
