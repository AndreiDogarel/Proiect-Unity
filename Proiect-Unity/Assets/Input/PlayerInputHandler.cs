using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    public PlayerMovement controller;

    private void Awake()
    {
        if (playerPrefabs[GameManager.instance.currentPlayerCount - 1] != null)
        {
            controller = GameObject.Instantiate(playerPrefabs[GameManager.instance.currentPlayerCount - 1], GameManager.instance.spawnPoints[0].transform.position, transform.rotation).GetComponent<PlayerMovement>();
            transform.parent = controller.transform;
            transform.position = controller.transform.position;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        controller.Move(context);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        controller.Jump(context);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        controller.Dash(context);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        controller.Attack(context);
    }

    public void RangeAttack(InputAction.CallbackContext context)
    {
        controller.RangeAttack(context);
    }

    public void Drop(InputAction.CallbackContext context)
    {
        controller.Drop(context);
    }
}
