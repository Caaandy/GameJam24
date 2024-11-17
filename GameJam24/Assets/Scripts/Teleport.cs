using UnityEngine;

public class Teleport : MonoBehaviour 
{
    [SerializeField] private GameObject _teleportTo;
    [SerializeField] private Transform _player;
    [SerializeField] private CameraMovement _cameraScript;


    private void OnTriggerEnter2D(Collider2D other)
    { 
        float distancePlayerCollider = transform.position.x - other.transform.position.x;
        Vector3 playerPos = new Vector3(_teleportTo.transform.position.x - distancePlayerCollider, _player.position.y, _player.position.z);
        _cameraScript.ResetCamera(playerPos);
        _player.position = playerPos;
    }
}