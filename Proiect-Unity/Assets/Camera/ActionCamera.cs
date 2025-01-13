using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class ActionCamera : MonoBehaviour
{
    public List<Transform> targets;

    [Header("Offset")]
    public Vector3 offset = new Vector3(1.23f, 3.08f, -0.5f);
    public float smoothTime = .5f;

    [Header("Zoom")]
    public float maxZoom = 40f;
    public float minZoom = 10f;
    public float zoomLimiter = 50f;

    [Header("Limits")]
    float minX = -15f;
    float minY = 0f;
    float maxX = 15f;
    float maxY = 9f;

    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        List<PlayerInput> playerList = GameManager.instance.playerList;
        if (playerList.Count > 2)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].GetComponent<PlayerInputHandler>().controller.GetComponent<PlayerStats>().livesLeft == 0)
                {
                    RemovePlayer(playerList[i]);
                } else
                {
                    AddPlayer(playerList[i]);
                }
            }
        }
    }

    void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);

        //float newZoom2 = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance2() / zoomLimiter);
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom2, Time.deltaTime);

    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;

    }

    float GetGreatestDistance2()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.y;

    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    // Methods to add and remove players
    public void AddPlayer(PlayerInput playerInput)
    {
        if (!targets.Contains(playerInput.transform))
        {
            targets.Add(playerInput.transform);
        }
    }

    public void RemovePlayer(PlayerInput playerInput)
    {
        targets.Remove(playerInput.transform);
    }
}