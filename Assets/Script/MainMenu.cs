using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject UI;
    public GameObject Menu;
    
    public GameObject Ennemy;
    public GameObject EnnemyCreator;
    public GameObject Fly;
    public GameObject FlyCreator;
    public GameObject FlyCreator2;

    public void PlayGame()
    {
        GameObject.Find("Perso").GetComponent<PlayerMovement>().isLaunched = true;
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            PlayGame();
            UI.SetActive(true);
            Menu.SetActive(false);
            Ennemy.SetActive(true);
            EnnemyCreator.SetActive(true);
            Fly.SetActive(true);
            FlyCreator.SetActive(true);
            FlyCreator2.SetActive(true);
        }
    }

    public void HyperLink(string url){
        Application.OpenURL(url);
    }

    public void appeared(GameObject bck){
        Color32 stk =  bck.GetComponent<Image>().color;
        bck.GetComponent<Image>().color =new Color32(stk.r,stk.g,stk.b,100);
    }

    public void disapear(GameObject bck){
        Color32 stk =  bck.GetComponent<Image>().color;
        bck.GetComponent<Image>().color =new Color32(stk.r,stk.g,stk.b,0);
    }


}
