using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;


public class InputText : MonoBehaviour
{
    public TextMeshProUGUI textInput;
    public TextMeshProUGUI warning1;
    public TextMeshProUGUI warning2;

static string CleanInput(string strIn)
{
    // Replace invalid characters with empty strings.
    return Regex.Replace(strIn,
          @"[^a-zA-Z0-9`!@#$%^&*()_+|\-=\\{}\[\]:"";'<>?,./]", ""); 
}

//Called when Input changes
public void inputValue()
{
    String trueString = textInput.text.Substring(0,textInput.text.Length-1);
    if(trueString.Length>3){
        warning1.fontStyle = FontStyles.Underline;
        return;
    }
    if(!Regex.IsMatch(trueString, @"^[a-zA-Z]+$")){
        warning2.fontStyle = FontStyles.Underline;
        return;
    }
    GameObject.Find("Perso").GetComponent<PlayerMovement>().SendScore(trueString);
}

    
}
