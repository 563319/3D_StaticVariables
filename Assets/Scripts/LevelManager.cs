using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public int highScore = 0;
    public int score = 0;
    public int playerHealth = 50;
    public int gameSessions = 0;

    public bool modeEasy = true;
    public bool modeMedium = false;
    public bool modeHard = false;
    public int modeChoice = 1;

    
    

    void Awake()
    {
        if (instance == null)
        {
            // if instance is null, store a reference to this instance
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("do not destroy");
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it
            // as we already have one
            print("do destroy");
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        
        if (modeEasy == true)
        {
            modeChoice = 1;
        }
        if (modeMedium == true)
        {
            modeChoice = 2;
        }
        if (modeHard == true)
        {
            modeChoice = 3;
        }
        print("difficulty: " + modeChoice);
    }


    //these methods are globally accessible
    public void SetHighScore(int score)
    {
        highScore = score;
    }
    public int GetHighScore()
    {
        return highScore;
    }
    
}