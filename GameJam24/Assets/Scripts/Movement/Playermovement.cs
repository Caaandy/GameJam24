using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Playermovement : MonoBehaviour
{
    public float jumpForce = 5;
    public int maxJumpsReset = 2;
    public int jumpsRemaining = 1;

    
    public float dashForce = 5;
    public bool dashUsed = false;
    
    public float dashTimeLimit = 2.0f;

    public int speed = 2;
    
    private Rigidbody2D _rb;
    
    public InputActionAsset inputActions;
    
    private InputAction _moveInputAction;
    private InputAction _jumpInputAction;
    private InputAction _dashInputAction;
    
    public sealed class States
    {
        public static readonly IMovementState IdleState    = new IdleState();
        public static readonly IMovementState WalkingState = new WalkingState();
        public static readonly IMovementState JumpingState = new JumpingState();
        public static readonly IMovementState FallingState = new FallingState();
        public static readonly IMovementState DashingState = new DashingState();

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
        
        _moveInputAction = inputActions.FindAction("Move");
        _jumpInputAction = inputActions.FindAction("Jump");
        _dashInputAction = inputActions.FindAction("Sprint");
        
        _jumpInputAction.performed += Jump;
        _dashInputAction.performed += Dash;
        
        States.IdleState.Initialize(this);
        States.WalkingState.Initialize(this);
        States.JumpingState.Initialize(this);
        States.FallingState.Initialize(this);
        States.DashingState.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        //Debug.Log(CurrentState);
        CurrentState.OnFixedUpdate();
    }

    void Jump(InputAction.CallbackContext context)
    {
        CurrentState.Jump(context);
    }

    void Dash(InputAction.CallbackContext context)
    {
        CurrentState.Dash(context);
    }
    
    public void ChangeState(IMovementState newState)
    {
        PreviousState = CurrentState;
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}
