using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider optionSlider;
    public void changeVolume(){
        AudioListener.volume = optionSlider.value;
    }
}
