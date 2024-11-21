using System;
using UnityEngine;
using UnityEngine.InputSystem;

//KeyCode.LeftShift

public class DashingState : IMovementState
{
    private Playermovement _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private InputActionAsset _inputActions;
    private InputAction _moveInputAction;
    private readonly int _layerMask = ~((1 << 2) + (1 << 6));

    //timer
    float timer = 0.0f;
    
    public void Initialize(Playermovement player)
    {
        _player = player;
        _rb = player.GetComponent<Rigidbody2D>();
        _spriteRenderer = player.GetComponent<SpriteRenderer>();
        _inputActions = player.inputActions;
        _moveInputAction = _inputActions.FindAction("Move");
    }

    public void OnEnter()
    {
        Debug.Log("dash");
        timer = _player.dashTimeLimit;
        _rb.linearVelocityY = 0;
        _rb.gravityScale = 0;
        var direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        _rb.AddForce(direction * _player.dashForce, ForceMode2D.Impulse);
        _player.dashUsed = true;
    }

    public void OnFixedUpdate()
    {
        if (timer <= 0)
        {
            _player.ChangeState(Playermovement.States.FallingState);
            return; 
        }
        
        // if (Physics2D.Raycast(_player.transform.position, Vector2.down, 1.01f, _layerMask))
        // {
        //     _player.grounded = true;
        //     _player.dashUsed = false;
        //     _player.jumpsRemaining = _player.maxJumpsReset;
        //     _player.ChangeState(Playermovement.States.IdleState);
        // }
        timer -= Time.fixedDeltaTime;
    }

    public void OnExit()
    {
        _rb.gravityScale = 1;
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
