using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{



    Sound sound;
    
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
        LevelManager.instance.playerHealth += 10;


    }
    public void FrontEnd()
    {
        SceneManager.LoadScene(0);
        
    }
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    
   
    public void PlaySFXtest()
    {
        AudioManager.instance.Play("reset", AudioManager.instance.sfxVolume);
       
    }



    //music

    public void ChangeMusicVolume(float volume)
    {
        //AudioManager.instance.musicVolume = PlayerPrefs.GetFloat("musicvol");
        AudioManager.instance.musicVolume = volume;
        //PlayerPrefs.Save();


    }

    //sfx
    public void ChangeSFXVolume(float volume)
    {
        //AudioManager.instance.sfxVolume = PlayerPrefs.GetFloat("sfxvol");
        AudioManager.instance.sfxVolume = volume;
        //PlayerPrefs.Save();
       
    }






    public void ApplyAudioChanges()
    {
        PlayerPrefs.SetFloat("sfxvol", AudioManager.instance.sfxVolume);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("musicvol", AudioManager.instance.musicVolume);
        PlayerPrefs.Save();

    }
}