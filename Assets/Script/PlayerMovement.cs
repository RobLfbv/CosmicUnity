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
    float maxJump = 1.3f;
    float minJump = 0.85f;
    float charger = 0.85f;
    float distToGround;
    bool isFallingBool;
    public HealthBar currentJump;
    bool isOnGround;
    private int score;
    public Text text;

    bool isJumping = false;

    public Animator animator;

    void Start(){
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        currentJump.SetJump(minJump);
    }
    // Update is called once per frame
    void Update()
    {
        score++;
        animator.SetBool("isOnGround",isOnGround);
        animator.SetBool("isLittle",down);
        animator.SetBool("discharge",isJumping);

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

        if(transform.position.y<=-7f){
            transform.position = new Vector3(transform.position.x,-6.31f,transform.position.z);
        }
        text.text = "" + score;
    }

    void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.tag == "Sol")
        {
            isOnGround = true;
            isJumping = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("BOOM");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            isOnGround = false;
            isJumping = true;
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
            GetComponent<BoxCollider2D>().size = new Vector2(0.28f,0.48f);
            rb.gravityScale = 20f;
            if(!down){
                transform.position = transform.position + new Vector3(0,-0.5f,0);
            }
            down = true;
        }else if(down){
            GetComponent<BoxCollider2D>().size = new Vector2(0.35f,0.7f);

            transform.position = transform.position + new Vector3(0,0.5f,0);
            rb.gravityScale = 0.4f;
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
