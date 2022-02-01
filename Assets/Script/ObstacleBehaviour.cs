using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public string originalName;

    void Update(){
        if(transform.position.x <=-40 && gameObject.name != originalName){
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if(!(gameObject.name == originalName && transform.position.x <=-50)){
            transform.position = transform.position + new Vector3(-0.05f,0,0);
        }
    }
}
