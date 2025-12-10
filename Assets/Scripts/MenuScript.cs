using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public Slider sliderMusic;
    public Slider sliderSFX;
    float musicVol;
    float sfxVol;

    
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
        
        AudioManager.instance.ChangeAudioSourceVolume("menumusic", AudioManager.instance.musicVolume);
        
        
    }
}
