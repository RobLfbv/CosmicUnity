using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolCreator : MonoBehaviour
{
    float respawn = 3.4f;

    public GameObject copyOf;
    void Start()
    {
        Invoke ("copy", respawn);
    }
 
    void copy()
    {
       //Do stuff
        Instantiate(copyOf, transform.position, transform.rotation);
        Invoke ("copy", respawn);
    }
}
