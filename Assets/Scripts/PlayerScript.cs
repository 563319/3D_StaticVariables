using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerScript : MonoBehaviour
{

    Rigidbody rb;
    /*
    AudioSource audioSource;
    public AudioClip sfx1;
    public AudioClip sfx2;// sound effect asset from sfx folder 
    //sfx1 can be an array of sounds 
    */
    public static bool reset;


    int speed = 8;
    int jumpSpeed = 7;
    int highScore;

    
    void Start()
    {

        //audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        reset = false;
    }

    
    void Update()
    {
        LevelManager.instance.SetHighScore(highScore);
        if (LevelManager.instance.score > highScore)
        {
            highScore = LevelManager.instance.score;
        }



        DoMove();
        DoRespawn();

        if (LevelManager.instance.playerHealth < 1)
        {
            SceneManager.LoadScene(0);
            LevelManager.instance.playerHealth = 50;

        }
        
    }

    void DoMove()
    {
        float yvel=rb.linearVelocity.y;

        float h = Input.GetAxis("Horizontal") * speed;
        float v = Input.GetAxis("Vertical") * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            yvel = jumpSpeed;
        }

        rb.linearVelocity = new Vector3 (h, yvel,v);




    }

    void DoRespawn()
    {
        if( reset )
        {
            //move player to reset point
            AudioManager.instance.PlayClip(1);
            transform.position = new Vector3(0, 1, 0);
            reset = false;
        }
    }
    private void OnGUI()
    {
        //read variable from LevelManager singleton
        int score = LevelManager.instance.GetHighScore();

        string text = "High score: " + score;

        text += "\nScore: " + LevelManager.instance.score;

        // define debug text area
        GUI.contentColor = Color.white;
        GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
        GUILayout.Label($"<size=24>{text}</size>");
        GUILayout.EndArea();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Pill")
        {
            LevelManager.instance.score += 1;
            Destroy(collision.gameObject);
            print(LevelManager.instance.score);
            //PlaySoundEffect();
            AudioManager.instance.PlayClip(0);

        }
    }
    /*
    void PlaySoundEffect()
    {
        audioSource.PlayOneShot(sfx1, 0.7f); // play audio clip with volume 0.7
    }
    void PlaySoundEffect2()
    {
        audioSource.PlayOneShot(sfx2, 0.7f); // play audio clip with volume 0.7
    }
    */

}
