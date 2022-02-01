using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCreator : MonoBehaviour
{
    bool bottom = false;
    float speed = 0.2f;
    void FixedUpdate()
    {
        if(transform.position.y>=-1.5 || !bottom){
            bottom = false;
            transform.position = transform.position + new Vector3(0,-1*speed,0);    
        }
        if(transform.position.y<=-6.36 || bottom){
            bottom = true;
            transform.position = transform.position + new Vector3(0,speed,0);

        }
        
    }
}
