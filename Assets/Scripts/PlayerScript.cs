using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerScript : MonoBehaviour
{

    Rigidbody rb;
    public static bool reset;


    int speed = 8;
    int jumpSpeed = 7;
    int tempHighScore;
    


    void Start()
    {
        ////////////////////////////////////////////////////////////
        
        if (PlayerPrefs.HasKey("gameSessions") == true)
        {
            LevelManager.instance.gameSessions = PlayerPrefs.GetInt("gameSessions");
        }
        else
        {
            PlayerPrefs.SetInt("gameSessions", 1);
        }
        LevelManager.instance.gameSessions += 1;
        PlayerPrefs.SetInt("gameSessions", LevelManager.instance.gameSessions);
        /////////////////////////////////////////////////////////////



        if (PlayerPrefs.HasKey("highScore") == true)
        {
            // the key highScore holds a value, therefore we can
            //retrieve it and store it in a variable
            LevelManager.instance.highScore = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            // the key highScore is null so give it a default value of 0 change to 1 to check if its working
            PlayerPrefs.SetInt("highScore", 0);
        }


        rb = GetComponent<Rigidbody>();
        reset = false;

    }

    
    void Update()
    {
        
        if (LevelManager.instance.score > LevelManager.instance.highScore)
        {
            LevelManager.instance.highScore = LevelManager.instance.score;
            PlayerPrefs.SetInt("highScore", LevelManager.instance.highScore);

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
        if (Input.GetKeyDown("-"))
        {
            LevelManager.instance.score -= 1;
        }
        if (Input.GetKeyDown("="))
        {
            LevelManager.instance.score += 1;
        }

        rb.linearVelocity = new Vector3 (h, yvel,v);




    }

    void DoRespawn()
    {
        if( reset )
        {
            //move player to reset point
            //AudioManager.instance.PlayClip(1);
            AudioManager.instance.Play("reset", AudioManager.instance.sfxVolume);

            transform.position = new Vector3(0, 1, 0);
            reset = false;
        }
    }
    private void OnGUI()
    {
        //read variable from LevelManager singleton
        int score = LevelManager.instance.highScore;

        string text = "High score: " + score;

        text += "\nScore: " + LevelManager.instance.score + "\n games sessions: " + LevelManager.instance.gameSessions;

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

            //AudioManager.instance.PlayClip(0);
            AudioManager.instance.Play("pickupcoin", AudioManager.instance.sfxVolume);

        }
    }
   

}
