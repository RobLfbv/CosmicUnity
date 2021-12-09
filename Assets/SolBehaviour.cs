using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolBehaviour : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(-0.1f,0,0);
        if(transform.position.x <= -100f){
            transform.position = transform.position + new Vector3(200f,0,0);
        }
    }
}
