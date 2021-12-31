using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    bool down = false;
    bool discharge = false;
    float jumpForce;
    Rigidbody2D rb;
    float maxJump = 1.8f;
    float minJump = 1.3f;
    float charger = 1.3f;
    float distToGround;
    bool isFallingBool;
    public HealthBar currentJump;
    bool isOnGround;
    private int score;
    public Text text;

    void Start(){
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        currentJump.SetJump(minJump);
    }
    // Update is called once per frame
    void Update()
    {
        score++;
        if(isOnGround && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z))){
            charger += Time.deltaTime;
            if (charger > maxJump)
            {
                currentJump.SetJump(maxJump);
            }
            else if (charger < minJump)
            {
                currentJump.SetJump(minJump);
            }
            else
            {
                currentJump.SetJump(charger);
            }
        }

        if (isOnGround && (Input.GetKeyUp(KeyCode.Space) ||Input.GetKeyUp(KeyCode.Z))){
            discharge = true;
            currentJump.SetJump(minJump);
        }
        text.text = "" + score;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            isOnGround = false;
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
            charger = minJump;
        }
        if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D)) && isFallingBool && !down){
            rb.gravityScale = 0.5f;
        }else if(!down){
            rb.gravityScale = 2f;
        }

    }

    void littleJack(){
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && rb.gravityScale != 0.5f){
            transform.localScale = new Vector3(transform.localScale.x, 0.25f, transform.localScale.z);
            rb.gravityScale = 100f;
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
