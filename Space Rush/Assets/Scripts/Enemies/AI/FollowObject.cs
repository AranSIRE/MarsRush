using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform target;
    public float speed;    

    public void FollowTarget(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
        
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        Vector3 targetDir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    
    public void RotateTowards(Vector3 target, float speed)
    {
        float step = speed * Time.deltaTime;
        Vector3 targetDirection = target - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0F);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDir), step);

       // transform.rotation = Quaternion.LookRotation(newDir);
    }
        /*

    public void RotateTowards(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
        float step = speed * Time.deltaTime;
        Vector3 targetDir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //transform.rotation = Quaternion.LookRotation(newDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDir), step);
    }

    */
}
