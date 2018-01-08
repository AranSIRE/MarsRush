using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Transform player;
    [SerializeField] private float speed = 3f;
    public Vector3 offset;
    public Parrallaxing paralax;

    private float cameraMinposY = -5.2f;
    private float cameraMaxposY = 7.2f;
    private float CamY;
    private float CamX;


    // Use this for initialization
    void Start () {		
        offset = transform.position - player.transform.position;
        paralax = paralax.GetComponent<Parrallaxing>();
    }
	
	// Update is called once per frame
	void LateUpdate () {

        float step = speed * Time.deltaTime;
        CamY = Mathf.Clamp(player.position.y, cameraMinposY, cameraMaxposY);

        if (paralax.getScrollspeed() > 0){
            CamX = 6;
        }
        else{
            CamX = -6;
        }
        offset = new Vector3(CamX, CamY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, offset, step);
                
    }

}
