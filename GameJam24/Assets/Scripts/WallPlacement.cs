using System;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class WallPlacement : MonoBehaviour
{
    #region References
    public GameObject wall;
    public Texture2D wallTexture;
    private GameObject wallInstance;
    public float wallWidth = 1.0f;
    private Vector2 wallSize;
    private Camera mainCamera;
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }
        if (wall != null)
        {
            wallSize = new Vector2(wallWidth * 2, 20.0f);
            wallInstance = Instantiate(wall);
            wallInstance.transform.parent = mainCamera.transform;
            var collider = wallInstance.GetComponent<BoxCollider2D>();
            collider.size = new Vector2(wallSize.x / 4.0f, wallSize.y);
            collider.offset = new Vector2(collider.size.x / 2.0f, 0.0f);
            wallInstance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            wallInstance.transform.position = CalculateWallPosition();
            var renderer = wallInstance.GetComponent<SpriteRenderer>();
            renderer.drawMode = SpriteDrawMode.Tiled;
            renderer.size = wallSize;
            renderer.sprite = Sprite.Create(wallTexture, new Rect(0.0f, 0.0f, wallTexture.width, wallTexture.height), new Vector2(0.0f, 0.5f), 32);
            renderer.sortingOrder = 1;
        }
        wallInstance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    void Update() {
        // For some reason it has a slight parallax effect, but we take it
        wallInstance.transform.position = new Vector3(wallInstance.transform.position.x, CalculateWallPosition().y - mainCamera.transform.position.y, wallInstance.transform.position.z);
    }

    public Vector3 CalculateWallPosition()
    {
        var wallScreenPosition = new Vector3(0, Screen.height / 2.0f, 0);
        var rawPosition = mainCamera.ScreenToWorldPoint(wallScreenPosition);
        return new Vector3(rawPosition.x, rawPosition.y, 0);
    }

    public void Reset()
    {
        Destroy(wallInstance);
        Start();
    }
}
