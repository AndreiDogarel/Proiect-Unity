using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public List<PlayerInput> playerList = new List<PlayerInput>();

    [SerializeField] InputAction joinAction;
    [SerializeField] InputAction leaveAction;

    public static GameManager instance = null;

    public event System.Action<PlayerInput> PlayerJoined;
    public event System.Action<PlayerInput> PlayerLeft;

    private int maxPlayerCount;
    public int currentPlayerCount = 0;

    // Reference to the ActionCamera script
    private ActionCamera actionCamera;

    private void Start()
    {
        maxPlayerCount = PlayerPrefs.GetInt("PlayerCount", 4);
        Debug.Log(maxPlayerCount);
    }

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        } else if (instance != null) 
        { 
            Destroy(gameObject);
        }

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        joinAction.Enable();
        joinAction.performed += context => JoinAction(context);

        leaveAction.Enable();
        leaveAction.performed += context => LeaveAction(context);

        // reference to camera
        actionCamera = Camera.main.GetComponent<ActionCamera>();

    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerList.Add(playerInput);

        if (PlayerJoined != null)
        {
            PlayerJoined(playerInput);
        }
        // Add player to camera players
        actionCamera.AddPlayer(playerInput);
    }

    void OnPlayerLeft(PlayerInput playerInput) 
    {
        // Remove player from camera players
        actionCamera.RemovePlayer(playerInput);

    }

    void JoinAction(InputAction.CallbackContext context)
    {
        currentPlayerCount++;
        if (currentPlayerCount <= maxPlayerCount)
        {
            PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
        }
    }

    void LeaveAction(InputAction.CallbackContext context)
    {
        if (playerList.Count > 1)
        {
            foreach (var player in playerList)
            {
                foreach (var device in player.devices)
                {
                    if (device != null && context.control.device == device)
                    {
                        UnregisterPlayer(player);
                        currentPlayerCount--;
                        return;
                    }
                }
            }
        }
    }

    void UnregisterPlayer(PlayerInput playerInput)
    {
        playerList.Remove(playerInput);

        if (PlayerLeft != null)
        {
            PlayerLeft(playerInput);
        }

        Destroy(playerInput.transform.parent.gameObject);
    }
}
