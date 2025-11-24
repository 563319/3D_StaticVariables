using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{

    public TextMeshProUGUI playerHealthText;
    

   
    void Start()
    {
        
    }

   
    void Update()
    {
        playerHealthText.text = LevelManager.instance.playerHealth + " HEALTH";
        

    }
}
