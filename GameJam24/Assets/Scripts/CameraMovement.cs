using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region References
    public GameObject player;
    public WallPlacement wallPlacement;
    public float smoothing = 1.0f;
    public float camHeightOffset = 0.0f;
    private Vector3 playerFurtherstPosition;
    private Vector3 camVelocity = Vector2.zero;
    private Camera mainCamera;

    //is this the first time we call start?
    private bool detlef = true;
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }
        if(!detlef)
        {
            Vector3 cameraDistance = playerFurtherstPosition - transform.position;
            transform.position = player.transform.position - cameraDistance;
            playerFurtherstPosition = player.transform.position;
        }
        if (player != null && detlef)
        {
            playerFurtherstPosition = player.transform.position;
            transform.position = CalculateTargetPosition(camHeightOffset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, CalculateTargetPosition(camHeightOffset), ref camVelocity, smoothing); // Smoothly move the camera to the player's position
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Make x only increase
            if (player.transform.position.x > playerFurtherstPosition.x)
            {
                playerFurtherstPosition = player.transform.position;
            }
            else
            {
                playerFurtherstPosition = new Vector3(playerFurtherstPosition.x, player.transform.position.y, player.transform.position.z);
            }
        }
    }

    private Vector3 CalculateTargetPosition(float offset = 0.0f)
    {
        return new Vector3(playerFurtherstPosition.x, playerFurtherstPosition.y + offset, -10.0f);
    }

    public void Reset()
    {
        detlef = false;
        wallPlacement.Reset();
        Start(); 
    }
}
