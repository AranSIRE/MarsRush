using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// Author@ Markus Turunen 
/// Modirfication date: 1.1.2018 - 6.1.2018 ©: Markus Turuen All rights Reserved

/// <summary>
/// This Script purpose is to control AI of the enemy characters.
/// 
/// </summary>

public class AI_EnemyFighter : MonoBehaviour {

    // Other GameObjects;
    private GameObject player;
    public Renderer GameMesh;
    private Transform playerPosition;
    public FollowObject follow;
    GameLogic gamelogic;

    //Enemy Initial Values_
    [SerializeField] private float speed = 30f;
    [SerializeField] private float projectileSpeed = 15f;
    [SerializeField] private float range = 15f;
    [SerializeField] private float responceTime = 1f;
    [SerializeField] private float RecoilSpeed = 0.5f;
    [SerializeField] private float Health = 1f;
    [SerializeField] private float tumble = 20f;
    enum EnemyState { alive, dead, ready, firing }
    EnemyState enemyState = EnemyState.alive;
        
    // VFX:
    [SerializeField] private GameObject ExplosionVFX;
    [SerializeField] private GameObject ExplosionVFX2;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject Projectile;

    // Flash Effect
    public Color NormalColour = Color.white;
    public Color FlashColour = Color.red;
    public float FlashDelay = 0.025f;
    public int TimesToFlash = 3;
    bool death = false;

    // Score Feature:
    private GameScore score;
    [SerializeField] private int ScoreAward = 100;

    
    // Initialize Variable
    void Start() {
        player = GameObject.Find("PlayerShip");
        playerPosition = player.transform;
        score = GameObject.Find("Score").GetComponent<GameScore>();
        gamelogic = GameObject.Find("Gamelogic").GetComponent<GameLogic>();
      
    }   

    // Increases dificulty of the enemy by increasing it's stats by indicated number.
    public void IncreaseStats(int i)
    {
        Health = Health + (+1 * i);
        speed =+ speed +(0.1f *i);
        projectileSpeed =+ projectileSpeed +(0.1f*i);
        if (responceTime > 0.1f){ responceTime =+ responceTime +(-0.1f*i);}
        if (RecoilSpeed > 0.1f) { RecoilSpeed =+RecoilSpeed + (-0.1f*i);}
        
    }

    public void ResetDificulty()
    {
        Health = 1;
        speed = 7f;
        projectileSpeed = 15f;
        responceTime=1;
        RecoilSpeed= 0.5f;
    }

    
    // Process to handle the Animation properties of the AI code
    private void Update()
    {
        if (enemyState != EnemyState.dead && gamelogic.Gamestate!= GameState.GameOver && playerPosition!=null)
        {
            if (MeasureDistance(transform.position, playerPosition.position) > range)
            {
                follow.FollowTarget(playerPosition, speed);
            }
            else
            {
                follow.RotateTowards(new Vector3(playerPosition.position.x, 
                playerPosition.position.y, playerPosition.position.z), speed);
                StartCoroutine("AimFire");
            }
        }
        else
        {            
            StartCoroutine("Death");
        }      
    }


    
    // This function handle's the Collisions events of the objects.
    void OnTriggerEnter(Collider other)
    {
        Health--;
        StartCoroutine("Flash");


        if (other.tag == "Projectile")
        {
            GameObject.Find("ObstacleGenerator").GetComponent<ObstacleGenerator>().ObjectiveDestroyed();
        }


        if (Health <= 0)
        {
            
            enemyState = EnemyState.dead;

            
        }

        if (other.tag.Equals("Obstacle") || other.tag.Equals("Enemy") )
        {
            Health = 0;
        }

        if (other.GetComponent<Asteroid>())
        {
            Health = 0;
        }

        




    }



    // Flash Effect That flashes when, the enemy is hit by anykind of collision.
    private IEnumerator Flash()
    {
        var renderer = GameMesh;
        if (renderer != null)
        {
            for (int i = 1; i <= TimesToFlash; i++)
            {
                renderer.material.color = FlashColour;
                yield return new WaitForSeconds(FlashDelay);
                renderer.material.color = NormalColour;
                yield return new WaitForSeconds(FlashDelay);
            }
        }
    }


    // AI Behavior for firing Projectiles 
    // T0D0 Fix the Firing Mechanics.
        IEnumerator AimFire()
        {
            if (enemyState != EnemyState.firing && enemyState!= EnemyState.dead)
            {
                enemyState = EnemyState.firing;                              
                yield return new WaitForSeconds(responceTime);
                Vector3 bulletTarget = (player.transform.position - SpawnPoint.transform.position).normalized;
                GameObject projectile = Instantiate(Projectile, SpawnPoint.transform.position,SpawnPoint.transform.rotation) as GameObject;
                projectile.GetComponent<Rigidbody>().velocity = new Vector3(bulletTarget.x,bulletTarget.y) * projectileSpeed;
                yield return new WaitForSeconds(RecoilSpeed);

            if (enemyState != EnemyState.dead)
                {
                    enemyState = EnemyState.ready;
                }            
            }
        }

    // Measure distance.
    private float MeasureDistance(Vector3 x, Vector3 y)
    {
        float z = Vector3.Distance(x,y);
        return z;
    }

    // This couroutine handles animation effects before this object is destroyed.
    public IEnumerator Death()
    {
       

        if (death == false)
        {
            death = true;
            GetComponent<Rigidbody>().angularVelocity = new Vector3(-tumble, 0, 0);
            GetComponent<Rigidbody>().velocity = new Vector3(-5, -10, 0);
            GetComponent<Rigidbody>().useGravity = true;
            Instantiate(ExplosionVFX2, transform.position, transform.rotation);
            Instantiate(ExplosionVFX, transform.position, transform.rotation);
            SpawnPoint.position = new Vector3(transform.position.x - 5f, transform.position.y - 10f, transform.position.z);
            follow.RotateTowards(new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, SpawnPoint.position.z), speed);
            yield return new WaitForSeconds(1f);
            score.CashInScores(ScoreAward);
            Destroy(gameObject);

        }
    }






}
