//using UnityEditorInternal;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    //[SerializeField] AudioMixer mixer;

    //const string MIXER_MUSIC = "MusicVolume";
    //const string MIXER_SFX = "SFXVolume";






    public static AudioManager instance;//=null;
    public float musicVolume = 1, sfxVolume = 1;
    //public AudioClip[] clips;
    //AudioSource audioSource; //reference to the audio source component on the game object
    

    public Slider sliderMusic;
    public Slider sliderSFX;

    public Sound[] sounds;
    void Awake()
    {
        print(Time.deltaTime);

        //DontDestroyOnLoad(gameObject);
        // if instance is null, store a reference to this instance
        if (instance == null)
        {
            // a reference does not exist, so store it
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("dont destroy audiomanager, instance=" + instance);
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it
            // as we already have one

            print("destroying audiomanager");

            //Destroy(gameObject);
            return;
        }
        ///
        
        ///
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = s.output;
        }






    }
    private void Start()
    {
        print("Audiomanager Start");



        if (PlayerPrefs.HasKey("musicvol") == true)
        {
            print("mw");
            musicVolume = PlayerPrefs.GetFloat("musicvol");
            
        }
        else
        {
            print("playerprefs set to 0.5 music");
            PlayerPrefs.SetFloat("musicvol", 0.5f);
        }


        if (PlayerPrefs.HasKey("sfxvol") == true)
        {
            print("mw");
            sfxVolume = PlayerPrefs.GetFloat("sfxvol");
        }
        else
        {
            print("playerprefs set to 0.5 sfx");
            PlayerPrefs.SetFloat("sfxvol", 0.5f);
        }


        



        Play("menumusic", musicVolume);



    }

    private void Update()
    {
        //mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }


    public void Play (string name, float vol )
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if ( s == null)
        {
            Debug.LogWarning("sound: " + name + " not found!");
            return;
        }
        s.source.volume = vol;
        s.source.Play();
    }
    public void ChangeAudioSourceVolume(string name, float vol)
    {
        Sound s = Array.Find(sounds, AudioSystem => AudioSystem.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found!");
            return;
        }
        s.source.volume = vol;


    }
}