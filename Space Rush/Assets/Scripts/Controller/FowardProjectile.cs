using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowardProjectile : MonoBehaviour {

    [SerializeField] private float lifeTime;    

    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        Destroy(this.gameObject, lifeTime);       
    }

}
