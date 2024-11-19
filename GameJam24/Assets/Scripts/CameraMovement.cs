using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region References
    public GameObject player;
    public WallPlacement wallPlacement;
    public float smoothing = 1.0f;
    public Vector2 positionOffset = new Vector2(0.0f, 0.0f);
    public float camDistanceZ = 10.0f;
    private Vector3 playerFurtherstPosition;
    private Vector3 camVelocity = Vector2.zero;
    private Camera mainCamera;
    private float distanceWorldMiddleToBottom = 0.0f;
    private float smoothingFactor = 1.0f;

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
            Vector3 cameraDistanceFromPlayer = playerFurtherstPosition - mainCamera.transform.position;
            mainCamera.transform.position = player.transform.position - cameraDistanceFromPlayer;
            playerFurtherstPosition = player.transform.position;
        }
        if (player != null && callStartFirstTime)
        {
            playerFurtherstPosition = player.transform.position;
            mainCamera.transform.position = CalculateTargetPosition(positionOffset);
        }
        var mid = new Vector3(Screen.width / 2, Screen.height / 2, -camDistanceZ);
        var bot = new Vector3(Screen.width / 2, 0, -camDistanceZ);
        var worldOffset = new Vector3(positionOffset.x, positionOffset.y, 0.0f);
        var worldMid = mainCamera.ScreenToWorldPoint(mid) - worldOffset;
        var worldBottom = mainCamera.ScreenToWorldPoint(bot) - worldOffset;
        distanceWorldMiddleToBottom = (worldMid - worldBottom).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, CalculateTargetPosition(positionOffset), ref camVelocity, smoothing * smoothingFactor); // Smoothly move the camera to the player's position
    }

    void FixedUpdate()
    {
        var distance = (playerFurtherstPosition - mainCamera.transform.position).magnitude;
        smoothingFactor = distanceWorldMiddleToBottom / distance;
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

    private Vector3 CalculateTargetPosition()
    {
        return CalculateTargetPosition(new Vector2(0.0f, 0.0f));
    }

    private Vector3 CalculateTargetPosition(Vector2 offset)
    {
        return new Vector3(playerFurtherstPosition.x + offset.x, playerFurtherstPosition.y + offset.y, -camDistanceZ);
    }

    public void ResetCamera(Vector3 lastPlayerPos)
    {
        callStartFirstTime = false;
        wallPlacement.Reset();
        Vector3 cameraDistanceFromPlayer = lastPlayerPos - mainCamera.transform.position;
        playerFurtherstPosition = player.transform.position;
        mainCamera.transform.position = player.transform.position - cameraDistanceFromPlayer;
        Start(); 
    }
}
