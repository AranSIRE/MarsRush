using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObject : MonoBehaviour {

  
    public GameObject object1;
    public GameObject object2;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    /*
	void Update () {

        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        if (MinDistance > distance)
        {
            transform.LookAt(object2.transform);
            transform.Rotate(0, 180, 0);
            transform.Translate(Vector3.forward);
        }
    }
    */
}
