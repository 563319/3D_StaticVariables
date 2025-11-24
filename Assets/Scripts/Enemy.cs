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
            PlayerScript.playerHealth -= 1;

        }
        /*
        if (PlayerScript.playerHealth < 1)
        {
            Destroy(gameObject);
        }
        */

    }
}
