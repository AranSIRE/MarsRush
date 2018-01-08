using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

// Author@ Markus Turunen 
/// Modirfication date: 1.1.2018 - 7.1.2018 ©: Markus Turuen All rights Reserved

/// <summary>
/// This Script purpose is to hande player Controls. 
/// 
/// </summary>

public partial class Controls :MonoBehaviour {
   
    // Player Movement Variables:
    public GameLogic gamelogic;
    public CameraState cameraState = CameraState.foward;  
    public Transform target;
    Vector3 targetVector;
    
    // DeathScene:
    [SerializeField] private GameObject ExplosionVFX;
    [SerializeField] private GameObject ExplosionVFX2;
    private float tumble = 20f;

    // Flash Effect: For Hit
    private Renderer GameMesh;
    public Color NormalColour = Color.white;
    public Color FlashColour = Color.red;
    public float FlashDelay = 0.025f;
    private int TimesToFlash = 3;

    // Parallaxing Feature;
    public GameObject paralaxObject;
    [SerializeField] private float accelerationRate = 0.001f;  
    private Parrallaxing paralax;     
    
    // Projectile Feature Variables:
    public GameObject Projectile;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private float speed;
    [SerializeField] private float projectileSpeed =20f;
    [SerializeField] private float recoilspeed = 0.3f;

    // Booleans:
    private bool recoil = false;
    private bool turn = false;

    // Initialize Variables
    void Start(){       
        paralax = paralaxObject.GetComponent<Parrallaxing>();
        gamelogic = FindObjectOfType<GameLogic>();
        GameMesh = GetComponent<Renderer>();
    }
   

    // Handle Input by each frame.
    void Update(){

        if (gamelogic.Gamestate == GameState.TitleScreen)
        {
            if (Input.GetMouseButton(0))
            {
                gamelogic.StartGame();
            }
        }
        if(gamelogic.Gamestate == GameState.Ingame)
        {
            if (Input.GetMouseButton(0))
            {
                targetVector = HandleInput();
                ModirfyScrolling(targetVector.x);
                StartCoroutine("Shoot");
            }
            RotateTowards(targetVector);
            HandleTransition(targetVector);
        }        

    }
    
    // This functions get Input from mouse cordinates and 
    // return Mouse Touch input as Vector3 in game Coordinates.
    public Vector3 HandleInput()
    {
        var pos = Input.mousePosition;
        pos.z = 10;
        pos = Camera.main.ScreenToWorldPoint(pos);
        target.position = pos;
        return target.position;
    }


    // This function modirfies the Speed of the player and Background based on input.
    void ModirfyScrolling(float x){
        if (x < 0){
            if (paralax.getScrollspeed() < 0){
                paralax.ModirfySpeed(-accelerationRate);
            }
            else{
                float LerpingValue = Mathf.Lerp(paralax.getScrollspeed(), -1f, 0.5f);
                paralax.SetScrollSpeed(LerpingValue);
                turn = true;
            }          
        }
        if (x > 0){
            if (paralax.getScrollspeed() > 0){
                paralax.ModirfySpeed(accelerationRate);
            }
            else{
                float LerpingValue = Mathf.Lerp(paralax.getScrollspeed(), 1f, 0.5f);
                paralax.SetScrollSpeed(LerpingValue);
                turn = true;
            }           
        }          
    }
    
    // This function handles Player Rotation towards the Input Rotation.
    public void RotateTowards(Vector3 target){
        float step = speed * Time.deltaTime;
        Vector3 targetDirection = target - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    
    // Handles Transition towards the input location.
    public void HandleTransition(Vector3 target){
        float step = speed * Time.deltaTime;
        Vector3 TargetY = new Vector3(0,target.y);
        transform.position = Vector3.MoveTowards(transform.position, TargetY, step);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject.Find("HealthBar").GetComponent<Health>().Hit();
    }

    public void Flash()
    {
        StartCoroutine("FlashEffect");
    }

    public void Die()
    {
        StartCoroutine("Death");
    }


    IEnumerator Shoot()
    {
        if (turn == true)
        {
            yield return new WaitForSeconds(0.5f);
        }
        if (recoil == false)
        {
            recoil = true;          
            GameObject projectile = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity) as GameObject;

            if (targetVector.x > 0)
            {
                projectile.transform.rotation = Quaternion.Euler(0, 0, -90);
                projectile.GetComponent<Rigidbody>().velocity = new Vector3(projectileSpeed, 0, 0); 
            }

            if (targetVector.x < 0)
            {
                projectile.transform.rotation = Quaternion.Euler(0, 0, 90);
                projectile.GetComponent<Rigidbody>().velocity = new Vector3(-projectileSpeed, 0, 0);
            }            

            yield return new WaitForSeconds(recoilspeed);         
            recoil = false;  
        }
        turn = false;
    }

    IEnumerator Death()
    {      
            StartCoroutine("FlashEffect");
            GetComponent<Rigidbody>().angularVelocity = new Vector3(-tumble, 0, 0);

            if (GetComponent<Controls>().cameraState.Equals(Controls.CameraState.foward))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(5, -10, 0);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(-5, -10, 0);
            }

            GetComponent<Rigidbody>().useGravity = true;
            Instantiate(ExplosionVFX2, transform.position, transform.rotation);
            Instantiate(ExplosionVFX, transform.position, transform.rotation);
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);        
    }

    IEnumerator FlashEffect()
    {
        var renderer = GameMesh;
        if (renderer != null)
            for (int i = 1; i <= TimesToFlash; i++)
            {
                renderer.material.color = FlashColour;
                yield return new WaitForSeconds(FlashDelay);
                renderer.material.color = NormalColour;
                yield return new WaitForSeconds(FlashDelay);
            }

    }

}
