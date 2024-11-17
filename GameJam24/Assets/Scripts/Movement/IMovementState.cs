using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovementState
{
    void Initialize(Playermovement player);
    void OnEnter();
    void OnFixedUpdate();
    void OnExit();
    void Jump(InputAction.CallbackContext context);
    void Dash(InputAction.CallbackContext context);
}
