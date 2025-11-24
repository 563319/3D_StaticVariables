using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    
    public static bool reset;


    int speed = 8;
    int jumpSpeed = 7;


    
    void Start()
    {
        LevelManager.instance.SetHighScore(50);

        rb = GetComponent<Rigidbody>();
        reset = false;
    }

    
    void Update()
    {

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

        //text += "\nThis is more text";

        // define debug text area
        GUI.contentColor = Color.white;
        GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
        GUILayout.Label($"<size=24>{text}</size>");
        GUILayout.EndArea();
    }


}
