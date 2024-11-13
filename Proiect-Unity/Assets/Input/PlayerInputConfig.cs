using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputConfig : MonoBehaviour
{
    void Start()
    {
        int playerCount = PlayerPrefs.GetInt("PlayerCount", 4); 
    
        if (PlayerInputManager.instance != null)
        {
            PlayerInputManager.instance.maxPlayerCount = playerCount;
        }
    }
}

