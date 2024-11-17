using System;
using UnityEngine;
using UnityEngine.InputSystem;

//KeyCode.LeftShift

public class DashingState : IMovementState
{
    private Playermovement _player;
    private Rigidbody2D _rb;
    private InputActionAsset _inputActions;
    private InputAction _moveInputAction;
    private readonly int _layerMask = ~((1 << 2) + (1 << 6));

    //timer
    float timer = 0.0f;

    
    public void Initialize(Playermovement player)
    {
        _player = player;
        _rb = player.GetComponent<Rigidbody2D>();
        _inputActions = player.inputActions;
        _moveInputAction = _inputActions.FindAction("Move");
    }

    public void OnEnter()
    {
        Debug.Log("dash");
    }

    public void OnFixedUpdate()
    {
        if (_rb.linearVelocityY <= 0)
        {
            timer+=Time.deltaTime;
            if (timer >= _player.dashTimeLimit)
            {
                _player.ChangeState(Playermovement.States.FallingState);
                return; 
            }
        }
        if (Physics2D.Raycast(_player.transform.position, Vector2.down, 1.01f, _layerMask))
        {
            _player.grounded = true;
            _player.dashUsed = false;
            _player.jumpsRemaining = _player.maxJumpsReset;
            _player.ChangeState(Playermovement.States.IdleState);
        }
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

    public void Dash(InputAction.CallbackContext context) {}
    
}
