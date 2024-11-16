using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : IMovementState
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
        
    }

    public void OnFixedUpdate()
    {
        if (!_moveInputAction.IsPressed())
        {
            _player.ChangeState(Playermovement.States.IdleState);
            return;
        }
        _rb.AddForce(new Vector2(_moveInputAction.ReadValue<float>(),0), ForceMode2D.Impulse);
        _rb.linearVelocityX = Math.Clamp(_rb.linearVelocityX, -2, 2);
    }

    public void OnExit()
    {
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_player.jumpsRemaining <= 0) return;
        _rb.linearVelocityY = 0;
        _rb.AddForce(Vector2.up * _player.jumpForce, ForceMode2D.Impulse);
        _player.jumpsRemaining -= 1; 
        _player.grounded = false;
        _player.ChangeState(Playermovement.States.JumpingState);
    }
}
