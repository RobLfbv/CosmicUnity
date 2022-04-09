using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    float speed;
    float speedRotation;
    float radius = 30f;
    float a;
    float b;
    float posX, posY;
    bool toLeft;
    void Start(){
        generateFunction();
    }
    // Update is called once per frame
    void Update()
    {   
        if(toLeft){
            posX = transform.position.x - speed;
            posY = a*posX + b;
        }else{
            posX = transform.position.x + speed;
            posY = a*posX + b;
        }
        transform.position = new Vector3(posX,posY,1);
        transform.Rotate (0,0,speedRotation*Time.deltaTime);         
        if(Mathf.Sqrt(Mathf.Pow(posX,2)+Mathf.Pow(posY,2))>=radius){
            generateFunction();
        }
    }
    void generateFunction(){
        float x = Random.Range(radius*-1,radius);
        float xO = Random.Range(-4f,10f);
        float yO = Random.Range(-8f,0f);
        float y = Mathf.Sqrt(Mathf.Pow(radius,2)-Mathf.Pow(x,2));
        float test = Random.Range(0f,1f);
        if(test>0.5){
            y = y*-1;
        }
        if(x>xO){
            a = (y-yO)/(x-xO);
            toLeft = true;
        }else{
            a = (yO-y)/(xO-x);
            toLeft =false;
        }
        b = y-a*x;
 
        speed = Random.Range(0.0001f,0.001f);
        transform.position = new Vector3(x,y,1);
        speedRotation = Random.Range(1f,5f);

    }
}
