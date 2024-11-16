using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float health = 5;
    public float damage = 1;
    
    public InputActionAsset inputActions;
    private InputAction _attackInputAction;

    [SerializeField]
    private GameObject lightningPrefab;
    
    private void OnEnable()
    {
        inputActions.Enable();
    }
    
    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        _attackInputAction = inputActions.FindAction("Attack");
        _attackInputAction.started += Attack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Attack(InputAction.CallbackContext context)
    {
        Instantiate(lightningPrefab, transform.position + Vector3.right * 1.5f, Quaternion.identity);
    }
}
