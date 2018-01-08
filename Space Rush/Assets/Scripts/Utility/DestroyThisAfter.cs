using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisAfter : MonoBehaviour {

    public float lifetime;
       
	void Start () {
        Destroy(gameObject, lifetime);
    }	

}
