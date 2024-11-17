using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Playermovement : MonoBehaviour
{
    public float jumpForce = 5;
    public int maxJumpsReset = 2;
    public int jumpsRemaining = 1;

    public int speed = 2;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    
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
    
    public bool grounded;
    
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
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
      CurrentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState.OnFixedUpdate();
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
