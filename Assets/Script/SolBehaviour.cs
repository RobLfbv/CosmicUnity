using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolBehaviour : MonoBehaviour
{
    public string originalName;
    void FixedUpdate()
    {
        if(transform.position.x <= -8.56f && gameObject.name != originalName){
            Destroy(gameObject);
        }else if(transform.position.x >= -8.56f){
            transform.position = transform.position + new Vector3(-0.05f,0,0);
        }
    }
}
