using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnnemyBehaviour : MonoBehaviour
{

    int minRespawn = 5;
    int maxRespawn = 10;

    public GameObject copyOf;
    void Start()
    {
        Invoke ("copy", (Random.Range(minRespawn, maxRespawn)));
    }
 
    void copy()
    {
        float delay = Random.Range (minRespawn, maxRespawn);
       //Do stuff
        Vector3 t = new Vector3(transform.position.x,transform.position.y,-1);
        Instantiate(copyOf, t, transform.rotation);
        Invoke ("copy", delay);
    }
}
