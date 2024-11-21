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
            _rb.linearVelocityX = 0;
            _player.ChangeState(Playermovement.States.IdleState);
            return;
        }
        _rb.linearVelocityX = _moveInputAction.ReadValue<float>() * _player.speed;
    }

    public void OnExit()
    {
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_player.jumpsRemaining <= 0) return;
        _player.ChangeState(Playermovement.States.JumpingState);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (_player.dashUsed) return;
        _player.ChangeState(Playermovement.States.DashingState);
    }
}
