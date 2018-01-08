using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrallaxing : MonoBehaviour {

    [SerializeField] private float scrollSpeed;    
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float minSpeed = -5f;
    private Vector2 savedOffset;

    void Start()
    {
        savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");      
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, savedOffset.y);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);        
    }

    void OnDisable()
    {
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
    
    public void ModirfySpeed(float speed)
    {
        if (scrollSpeed + speed < maxSpeed && scrollSpeed +speed > minSpeed)
        {
            scrollSpeed = scrollSpeed + speed;
            scrollSpeed = Mathf.Lerp(scrollSpeed, scrollSpeed + speed, 1f);
        }        
    }

    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }

    public float getScrollspeed()
    {
        return scrollSpeed;
    }
}



