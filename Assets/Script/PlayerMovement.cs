using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.IO;
using System;


public class PlayerMovement : MonoBehaviour
{
    bool down = false;
    bool discharge = false;
    float jumpForce;
    Rigidbody2D rb;
    float maxJump = 1.3f;
    float minJump = 0.85f;
    float charger = 0.85f;
    float gravityDown = 15f;
    float gravitySide = 0.1f;
    float gravity = 2f;
    float distToGround;
    bool isFallingBool;
    public HealthBar currentJump;
    bool isOnGround;
    private int score;
    public Text text;
    bool isJumping = false;

    public Animator animator;

    public bool isLaunched = false;
    public bool isAlive = true;
    public GameObject endScreen;

    public TextMeshProUGUI textScoreDeath;
    public TextMeshProUGUI highScore;

    public AudioSource audioData;

    private const string url = "https://comsicjackbase.000webhostapp.com/config.php";

    void Start(){
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        currentJump.SetJump(minJump);
        isAlive = true;
    }
    // Update is called once per frame
    void Update()
    {   
        if(isLaunched && isAlive){
            score++;
        }
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
        if(isLaunched && isAlive){
            text.text = "" + score;
        }
        if(score%1000==0 && score!=0){
            audioData.Play(0);
        }
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
            isAlive = false;
            onDeath();
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
            rb.gravityScale = gravitySide;
        }else if(!down){
            rb.gravityScale = gravity;
        }

    }

    void littleJack(){
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && rb.gravityScale != 0.5f){
            GetComponent<BoxCollider2D>().size = new Vector2(0.28f,0.48f);
            rb.gravityScale = gravityDown;
            /*if(!down){
                transform.position = transform.position + new Vector3(0,-0.5f,0);
            }*/
            down = true;
        }else if(down){
            GetComponent<BoxCollider2D>().size = new Vector2(0.35f,0.7f);
            //transform.position = transform.position + new Vector3(0,0.5f,0);
            rb.gravityScale = gravity;
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

 void onDeath(){
     endScreen.SetActive(true);
     textScoreDeath.text = ""+score;
     RetrieveScores();
     Time.timeScale = 0;
 }

    public void SendScore(string name){
        //SetScore(name);
        PostScores(name,score);
    }
    void SetScore(string name){
        /*
        if(score>PlayerPrefs.GetInt("high10int")){
            if(score<PlayerPrefs.GetInt("high9int")){
                PlayerPrefs.SetInt("high10int",score);
                PlayerPrefs.SetString("high10string",name);
                return;
            }
            for(int i = 1; i<9; i++){
                if(score>PlayerPrefs.GetInt("high"+i+"int")){
                    int scoreStock =PlayerPrefs.GetInt("high"+i+"int");
                    string nameStock =PlayerPrefs.GetString("high"+i+"string");
                    PlayerPrefs.SetInt("high"+i+"int",score);
                    PlayerPrefs.SetString("high"+i+"string",name);
                    for(int j = (i+1); j<=10; j++){
                        int scoreStock2 = PlayerPrefs.GetInt("high"+j+"int");
                        string nameStock2 = PlayerPrefs.GetString("high"+j+"string");
                        PlayerPrefs.SetInt("high"+j+"int",scoreStock);
                        PlayerPrefs.SetString("high"+j+"string",nameStock);
                        scoreStock = scoreStock2;
                        nameStock = nameStock2;
                    }
                    return;
                }
            }
        }*/
    }

    void GetScore(){
        /*highScore.text = "HIGHSCORES\n";
        for(int i = 1; i<=10; i++){
            if(i<10){
                highScore.text+="0"+i+" "+ PlayerPrefs.GetString("high"+i+"string") +" : "+PlayerPrefs.GetInt("high"+i+"int")+"\n";
            }else{
                highScore.text+=i+" "+ PlayerPrefs.GetString("high"+i+"string") +" : "+PlayerPrefs.GetInt("high"+i+"int")+"\n";
            }
        }*/
        List<Score> l = RetrieveScores();

    }

    public List<Score> RetrieveScores()
    {
        List<Score> scores = new List<Score>();
        StartCoroutine(DoRetrieveScores(scores));
        return scores;
    }
     IEnumerator DoRetrieveScores(List<Score> scores)
    {
        WWWForm form = new WWWForm();
        form.AddField("retrieve_leaderboard", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully retrieved scores!");
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Score entry = new Score();
                        entry.name = line;
                        try
                        {
                            entry.score = Int32.Parse(reader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Invalid score: " + e);
                            continue;
                        }

                        scores.Add(entry);
                    }
                }
                highScore.text = "HIGHSCORES\n";
                for(int i = 0; i<10; i++){
                    highScore.text+=""+scores[i].name+" : "+scores[i].score+"\n";
                }
            }
        }
    }
    public void PostScores(string name, int score)
    {
        StartCoroutine(DoPostScores(name, score));
    }
     IEnumerator DoPostScores(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard", "true");
        form.AddField("name", name);
        form.AddField("score", score);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.result);
                RetrieveScores();
                Debug.Log("Successfully posted score!");
            }
        }
    }
    public struct Score
    {
        public string name;
        public int score;
    }
}   
