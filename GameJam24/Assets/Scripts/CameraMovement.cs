using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region References
    public GameObject player;
    public GameObject wall;
    public float smoothing = 1.0f;
    private GameObject wallInstance;
    public float wallWidth = 1.0f;
    public float camHeightOffset = 0.0f;
    private Vector3 playerFurtherstPosition;
    private Vector3 camVelocity = Vector2.zero;
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (wall != null)
        {
            var wallSize = new Vector3(wallWidth, Screen.height, 0); 
            wallInstance = Instantiate(wall);
            wallInstance.GetComponent<BoxCollider2D>().size = wallSize;
            wallInstance.transform.position = CalculateWallPosition();
            wallInstance.transform.localScale = new Vector3(wallWidth, Screen.height, 1.0f);
        }
        if (player != null)
        {
            playerFurtherstPosition = player.transform.position;
            transform.position = CalculateTargetPosition(camHeightOffset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, CalculateTargetPosition(camHeightOffset), ref camVelocity, smoothing); // Smoothly move the camera to the player's position
        wallInstance.transform.position = CalculateWallPosition();
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

    private Vector3 CalculateWallPosition()
    {
        var wallScreenPosition = new Vector3(- wallWidth / 2.0f, Screen.height / 2.0f, player.transform.position.z);
        var rawPosition = Camera.main.ScreenToWorldPoint(wallScreenPosition);
        return new Vector3(rawPosition.x, rawPosition.y, player.transform.position.z);
    }

    public void Reset()
    {
        GameObject.Destroy(wallInstance);
        Start();
    }
}
