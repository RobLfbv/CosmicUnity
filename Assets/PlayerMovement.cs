using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool down = false;
    float charger = 0;
    bool discharge = false;
    float jumpForce;
    Rigidbody2D rb;
    float maxJump = 1.8f;
    float distToGround;
    bool isGrounded;

    void Start(){
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z)){
            charger += Time.deltaTime;
            Debug.Log(charger);
        }

        if(Input.GetKeyUp(KeyCode.Space) ||Input.GetKeyUp(KeyCode.Z)){
            discharge = true;
        }
        
    }

    void FixedUpdate(){
        littleJack();
        if(discharge){
            if(charger>maxJump){
                charger = maxJump;
            }
            jumpForce = 10 * charger;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            discharge = false;
            charger = 0f;
        }

        if(!isGrounded && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.D))){
            rb.gravityScale = 0.5f;
            Debug.Log("Hello");
        }
    }

    void littleJack(){
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            transform.localScale = new Vector3(transform.localScale.x, 0.25f, transform.localScale.z);
            if(!down){
                transform.position = transform.position + new Vector3(0,-0.5f,0);
            }
            down = true;
        }else if(down){
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            transform.position = transform.position + new Vector3(0,0.5f,0);
            down = false;
        }
    }

 //make sure u replace "floor" with your gameobject name.on which player is standing
 void OnCollisionEnter(Collision theCollision)
 {
     if (theCollision.gameObject.name == "Sol1" || theCollision.gameObject.name == "Sol2")
     {
         isGrounded = true;

     }
 }
 
 //consider when character is jumping .. it will exit collision.
 void OnCollisionExit(Collision theCollision)
 {
     if (theCollision.gameObject.name == "Sol1" ||theCollision.gameObject.name == "Sol2")
     {
         isGrounded = false;
     }
 }

}   
