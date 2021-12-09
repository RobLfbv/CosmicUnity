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
    float minJump = 1.3f;
    float distToGround;
    bool isFallingBool;

    void Start(){
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z)){
            charger += Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.Space) ||Input.GetKeyUp(KeyCode.Z)){
            discharge = true;
        }
        
    }

    void FixedUpdate(){
        littleJack();
        isFalling();
        if(discharge){
            if(charger>maxJump){
                charger = maxJump;
            }
            if(charger<minJump){
                charger = minJump;
            }
            jumpForce = 10 * charger;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            discharge = false;
            charger = 0f;
        }
        if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D)) && isFallingBool){
            rb.gravityScale = 0.5f;
        }else{
            rb.gravityScale = 2f;
        }

    }

    void littleJack(){
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            transform.localScale = new Vector3(transform.localScale.x, 0.25f, transform.localScale.z);
            rb.gravityScale = 3f;
            Debug.Log(rb.gravityScale);
            if(!down){
                transform.position = transform.position + new Vector3(0,-0.5f,0);
            }
            down = true;
        }else if(down){
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            transform.position = transform.position + new Vector3(0,0.5f,0);
            rb.gravityScale = 2f;
            down = false;
        }
    }

 void isFalling(){
    if (GetComponent<Rigidbody2D>().velocity.y < -0.1)
    {
        isFallingBool = true;
    }
    else
    {
        isFallingBool = false;
    }
 }

}   
