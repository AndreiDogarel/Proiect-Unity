using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputConfig : MonoBehaviour
{
    private int maxPlayerCount;
    private int currentPlayerCount = 0;

    void Start()
    {
        maxPlayerCount = PlayerPrefs.GetInt("PlayerCount", 4);
        Debug.Log(maxPlayerCount);

        if (PlayerInputManager.instance != null)
        {
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
        }
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        if (currentPlayerCount >= maxPlayerCount)
        {
            Destroy(player.gameObject);
            Debug.Log("You have reached maximum number of players!");
            return;
        }

        currentPlayerCount++;
    }

    private void OnDisable()
    {
        if (PlayerInputManager.instance != null)
        {
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
        }
    }
}

