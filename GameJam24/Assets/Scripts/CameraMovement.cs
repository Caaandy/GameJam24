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

    private bool callStartFirstTime = true;
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }
        if(!callStartFirstTime)
        {
            Vector3 cameraDistance = playerFurtherstPosition - mainCamera.transform.position;
            mainCamera.transform.position = player.transform.position - cameraDistance;
            playerFurtherstPosition = player.transform.position;
        }
        if (player != null && callStartFirstTime)
        {
            playerFurtherstPosition = player.transform.position;
            mainCamera.transform.position = CalculateTargetPosition(camHeightOffset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, CalculateTargetPosition(camHeightOffset), ref camVelocity, smoothing); // Smoothly move the camera to the player's position
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

    public void ResetCamera(Vector3 lastPlayerPos)
    {
        callStartFirstTime = false;
        wallPlacement.Reset();
        Vector3 cameraDistance = lastPlayerPos - mainCamera.transform.position;
        playerFurtherstPosition = player.transform.position;
        mainCamera.transform.position = player.transform.position - cameraDistance;
        Start(); 
    }
}
