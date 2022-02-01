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
        Instantiate(copyOf, transform.position, transform.rotation);
        Invoke ("copy", delay);
    }
}
