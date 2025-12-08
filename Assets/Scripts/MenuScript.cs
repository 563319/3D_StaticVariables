using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public Slider sliderMusic;
    public Slider sliderSFX;
    float musicVol;
    float sfxVol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //read playerprefs value and put into musicSlider.value
        musicVol = PlayerPrefs.GetFloat("musicvol");
        sfxVol = PlayerPrefs.GetFloat("sfxvol");
        sliderMusic.value = musicVol;
        sliderSFX.value = sfxVol;
    }

    // Update is called once per frame
    void Update()
    {
        //music
        AudioManager.instance.ChangeAudioSourceVolume("menumusic", AudioManager.instance.musicVolume);
        //sfx
        //AudioManager.instance.ChangeAudioSourceVolume("pickupcoin", AudioManager.instance.sfxVolume);
        //AudioManager.instance.ChangeAudioSourceVolume("reset", AudioManager.instance.sfxVolume);
        
    }
}
