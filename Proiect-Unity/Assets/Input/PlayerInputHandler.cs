using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement controller;

    Vector3 startPos = new Vector3(0, 0, 0);

    private void Awake()
    {
        if (player != null) 
        {
            controller = GameObject.Instantiate(player, startPos, transform.rotation).GetComponent<PlayerMovement>();
            transform.parent = controller.transform;
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
}
