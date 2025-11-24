using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //public variable to reference the button text - set this up in the Unity editor
    public TMP_Text buttonText;

    public void ButtonMethod()
    {
        buttonText.text = "Clicked!";
    }
    public void ButtonResetPlayer()
    {
        LevelManager.instance.playerHealth = 50;
        LevelManager.instance.score = 0;
        PlayerScript.reset = true;

        print("doing reset");
        

    }
    public void ButtonHeal()
    {
        LevelManager.instance.playerHealth += 1;


    }
    public void FrontEnd()
    {
        SceneManager.LoadScene(0);
    }
}