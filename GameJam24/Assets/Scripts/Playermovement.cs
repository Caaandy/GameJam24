using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    public float jumpForce = 5;
    public int jumpsRemaining = 1;
    
    private Rigidbody2D _rb;
    
    public InputActionAsset inputActions;
    
    private InputAction _moveInputAction;
    private InputAction _jumpInputAction;
    
    public sealed class States
    {
        public static readonly IMovementState IdleState    = new IdleState();
        public static readonly IMovementState WalkingState = new WalkingState();
        public static readonly IMovementState JumpingState = new JumpingState();
        public static readonly IMovementState FallingState = new FallingState();
    };
    
    public IMovementState CurrentState = States.IdleState;
    public IMovementState PreviousState = States.IdleState;
        
    
    private readonly int _layerMask = ~((1 << 2) + (1 << 6));
    public bool _grounded;
    private float _groundedTimer = 0.05f;
    private float _groundedTimerMax = 0.05f;
    
    
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
        
        _jumpInputAction.performed += Jump;
        
        States.IdleState.Initialize(this);
        States.WalkingState.Initialize(this);
        States.JumpingState.Initialize(this);
        States.FallingState.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (_groundedTimer > _groundedTimerMax && Physics2D.Raycast(transform.position, Vector2.down, 1.01f, _layerMask))
        {
            _grounded = true;
            jumpsRemaining = 1;
            _groundedTimer = 0;
        }
        CurrentState.OnUpdate();
        _rb.AddForce(new Vector2(_moveInputAction.ReadValue<float>(),0), ForceMode2D.Impulse);
        _rb.linearVelocityX = Math.Clamp(_rb.linearVelocityX, -2, 2);
        
        _groundedTimer += Time.deltaTime;
    }
    
    void Jump(InputAction.CallbackContext context)
    {
        CurrentState.Jump(context);
    }
    
    public void ChangeState(IMovementState newState)
    {
        PreviousState = CurrentState;
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}
