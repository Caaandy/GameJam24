using UnityEngine;

public class Teleport : MonoBehaviour 
{
    [SerializeField] private GameObject _teleportTo;
    [SerializeField] private Transform _player;
    [SerializeField] private CameraMovement _cameraScript;


    private void OnTriggerEnter2D(Collider2D other)
    { 
        _player.position = new Vector3(_teleportTo.transform.position.x, _player.position.y, _player.position.z);
        _cameraScript.Reset();
    }
}
