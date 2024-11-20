using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement controller;

    Vector3 startPos = new Vector3(0, 0, 0);

    private void Awake()
    {
        if (player != null) 
        {
            controller = GameObject.Instantiate(player, GameManager.instance.spawnPoints[0].transform.position, transform.rotation).GetComponent<PlayerMovement>();
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

}
