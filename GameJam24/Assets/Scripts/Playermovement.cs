using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    [SerializeField]
    private InputActionAsset inputActions;
    
    private InputAction _moveInputAction;
    private InputAction _jumpInputAction;

    private readonly int _layerMask = ~((1 << 2) + (1 << 6));
    public bool _grounded;
    
    void Awake()
    {
        
    }

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
        _rb = GetComponent<Rigidbody2D>();
        
        _moveInputAction = inputActions.FindAction("Move");
        _jumpInputAction = inputActions.FindAction("Jump");
        
        _jumpInputAction.started += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        _grounded = Physics2D.Raycast(transform.position + Vector3.right, Vector2.down, 1.01f, _layerMask) || 
                    Physics2D.Raycast(transform.position + Vector3.left, Vector2.down, 1.01f, _layerMask);
        _rb.AddForce(new Vector2(_moveInputAction.ReadValue<Vector2>().x,0), ForceMode2D.Impulse);
        _rb.linearVelocityX = Math.Clamp(_rb.linearVelocityX, -2, 2);
    }
    
    void Jump(InputAction.CallbackContext context)
    {
        if (_grounded)
        {
            _rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
