using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    void Start()
    { 
        
    }

   
    void Update()
    {
       
            

        

        
    }

    void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.tag == "Player")
        {
            LevelManager.instance.playerHealth -= 10;
            

        }
        

    }
}
