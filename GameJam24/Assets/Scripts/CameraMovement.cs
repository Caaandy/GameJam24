using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region References
    public GameObject player;
    public GameObject wall;
    public float smoothing = 1.0f;
    private Vector2 playerFurtherstPosition;
    private Vector2 camVelocity = Vector2.zero;
    #endregion

    #region Constants
    private float wallWidth = 1.0f;
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var wallScreenPosition = new Vector3(wallWidth / 2.0f, Screen.height / 2.0f, player.transform.position.z);
        var wallWorldBounds = new Bounds(Camera.main.ScreenToWorldPoint(wallScreenPosition), new Vector3(wallWidth, Screen.height, 0));
        if (wall != null)
        {
            wall.GetComponent<SpriteRenderer>().bounds = wallWorldBounds;
        }
        if (player != null)
        {
            playerFurtherstPosition = player.transform.position;
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, player.transform.position, ref camVelocity, smoothing); // Smoothly move the camera to the player's position
    }

    void FixedUpdate() {
        if (player != null)
        {
            // Make x only increase
            if (player.transform.position.x > playerFurtherstPosition.x)
            {
                playerFurtherstPosition = player.transform.position;
            }
            else
            {
                playerFurtherstPosition = new Vector2(playerFurtherstPosition.x, player.transform.position.y);
            }
        }
    }
}
