using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndScreen : MonoBehaviour
{
    public void Retry(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            Retry();
        }
    }
}
