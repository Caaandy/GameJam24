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
        _player.animator.SetTrigger("Jump");
    }

    public void OnUpdate()
    {
        float moveDirection = _moveInputAction.ReadValue<float>();
        if (moveDirection != 0) {
            _player.spriteRenderer.flipX = moveDirection < 0;
        }
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
        _player.animator.ResetTrigger("Jump");
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
}

