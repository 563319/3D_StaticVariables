using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";

    Sound sound;
    
    //public variable to reference the button text - set this up in the Unity editor
    public TMP_Text buttonText;

    public void ButtonMethod()
    {
        buttonText.text = "Clicked!";
        AudioManager.instance.Play("ButtonClick1", AudioManager.instance.sfxVolume);
    }
    public void ButtonResetPlayer()
    {
        AudioManager.instance.Play("ButtonClick3", AudioManager.instance.sfxVolume);
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
    ///UI SFX
    public void PlaySFXUI1()
    {
        AudioManager.instance.Play("ButtonClick1", AudioManager.instance.sfxVolume);
    }
    public void PlaySFXUI2()
    {
        AudioManager.instance.Play("ButtonClick2", AudioManager.instance.sfxVolume);
    }
    public void PlaySFXUI3()
    {
        AudioManager.instance.Play("ButtonClick3", AudioManager.instance.sfxVolume);
    }



    public void PlaySFXtest()
    {
        AudioManager.instance.Play("reset", AudioManager.instance.sfxVolume);
       
    }

    //music

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.instance.musicVolume = volume;
       mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }

    //sfx
    public void ChangeSFXVolume(float volume)
    {
        AudioManager.instance.sfxVolume = volume;
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
    }

    public void ApplyAudioChanges()
    {
        PlayerPrefs.SetFloat("sfxvol", AudioManager.instance.sfxVolume);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("musicvol", AudioManager.instance.musicVolume);
        PlayerPrefs.Save();
    }




    ///difficulty
    public void SetDifficultyEasy()
    {
       // LevelManager.instance.modeEasy = true;
       // LevelManager.instance.modeMedium = false;
       // LevelManager.instance.modeHard = false;
        LevelManager.instance.modeChoice = 1;
    }
    public void SetDifficultyMedium()
    {
        //LevelManager.instance.modeMedium = true;
       // LevelManager.instance.modeEasy = false;
       // LevelManager.instance.modeHard = false;
        LevelManager.instance.modeChoice = 2;
    }
    public void SetDifficultyHard()
    {
       // LevelManager.instance.modeHard = true;
       // LevelManager.instance.modeEasy = false;
        //LevelManager.instance.modeMedium = false;
        LevelManager.instance.modeChoice = 3;
    }
    public void ApplyDifficultyChanges()
    {
        
        PlayerPrefs.SetInt("DifficultyChoice", LevelManager.instance.modeChoice);
        PlayerPrefs.Save();
        print("difficulty applied: " + LevelManager.instance.modeChoice);
    }

}