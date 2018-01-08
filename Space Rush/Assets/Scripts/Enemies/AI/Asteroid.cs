using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    
    public float tumble = 20f;
    private float speed = 50f;
    private float lifetime = 5f;

    public GameObject explosionVFX;
    
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        GetComponent<Rigidbody>().velocity = new Vector3(-0.5f, 0, 0) * speed;
        Destroy(gameObject, lifetime);
    }
    void OnTriggerEnter(Collider other)
    {
        Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(gameObject);
       
    }
}
