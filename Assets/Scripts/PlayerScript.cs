using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    
    public static bool reset;


    int speed = 8;
    int jumpSpeed = 7;
    int highScore;

    
    void Start()
    {
        

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
            Destroy(gameObject);

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

        }
    }
}
