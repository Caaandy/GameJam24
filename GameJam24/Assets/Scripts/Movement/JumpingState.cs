using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingState : IMovementState
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
        if (_rb.linearVelocityY <= 0)
        {
            _player.ChangeState(Playermovement.States.FallingState);
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
        _rb.linearVelocityY = 0;
        _rb.linearVelocityX = Math.Clamp(_rb.linearVelocityX, -2, 2);
        _rb.AddForce(Vector2.up * _player.jumpForce, ForceMode2D.Impulse);
        _player.jumpsRemaining -= 1;
        _player.grounded = false;
        _player.ChangeState(Playermovement.States.JumpingState);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (_player.dashUsed) return;

        if(_moveInputAction.ReadValue<float>() != 0f) {
            _rb.AddForce(Vector2.right * _moveInputAction.ReadValue<float>() * _player.dashForce, ForceMode2D.Impulse);
            Debug.Log(Vector2.right * _moveInputAction.ReadValue<float>() * _player.dashForce);
        }
        else{
            _rb.AddForce(Vector2.right * _player.dashForce, ForceMode2D.Impulse);
        }
        _player.dashUsed = true;
        _player.ChangeState(Playermovement.States.DashingState);
    }
}

