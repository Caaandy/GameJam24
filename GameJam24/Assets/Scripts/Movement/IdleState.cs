using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : IMovementState
{
    private Playermovement _player;
    private Rigidbody2D _rb;
    private InputActionAsset _inputActions;
    private InputAction _moveInputAction;

    public void Initialize(Playermovement player)
    {
        _player = player;
        _rb = player.GetComponent<Rigidbody2D>();
        _inputActions = player.inputActions;
        _moveInputAction = _inputActions.FindAction("Move");
    }

    public void OnEnter()
    {
        _player.animator.SetTrigger("Idle");
    }

    public void OnUpdate()
    {
        
    }

    public void OnFixedUpdate()
    {
        if (_moveInputAction.IsPressed())
        {
            _player.ChangeState(Playermovement.States.WalkingState);
        }
    }

    public void OnExit()
    {
        _player.animator.ResetTrigger("Idle");
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_player.jumpsRemaining <= 0) return;
        _rb.AddForce(Vector2.up * _player.jumpForce, ForceMode2D.Impulse);
        _player.jumpsRemaining -= 1; 
        _player.grounded = false;
        _player.ChangeState(Playermovement.States.JumpingState);
    }
}

