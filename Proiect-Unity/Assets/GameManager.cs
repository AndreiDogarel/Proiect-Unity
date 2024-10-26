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

    //For every player keeps track if they are alive
    public Dictionary<int, bool> lifeSupportForPlayers = new Dictionary<int, bool>();

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
        Debug.Log("Test");

        leaveAction.Enable();
        leaveAction.performed += context => LeaveAction(context);

    }

    private void Start()
    {
        PlayerInputManager.instance.JoinPlayer(0, -1, null);
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        if (!lifeSupportForPlayers.ContainsKey(playerInput.playerIndex))
        {
            playerList.Add(playerInput);
            lifeSupportForPlayers.Add(playerInput.playerIndex, true);

            if (PlayerJoined != null)
            {
                PlayerJoined(playerInput);
            }
            Debug.Log("Playerul a intra! NOU " + playerInput);
            /*Debug.Log(lifeSupportForPlayers.Count);
            Debug.Log(playerInput.name);
            Debug.Log(playerInput.playerIndex);
            Debug.Log(playerInput.tag);
            Debug.Log(playerInput.user);*/
        }
    }

    void OnPlayerLeft(PlayerInput playerInput) 
    { 

    }

    void JoinAction(InputAction.CallbackContext context)
    {
        PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
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

    public void KillPlayer(PlayerInput playerInput)
    {
        if(playerInput != null)
        {
            if (lifeSupportForPlayers.ContainsKey(playerInput.playerIndex))
            {
                Debug.Log("Am sters playerul: " + playerInput.playerIndex);
                lifeSupportForPlayers[playerInput.playerIndex] = false;
                UnregisterPlayer(playerInput);
            }
        }
    }
}
