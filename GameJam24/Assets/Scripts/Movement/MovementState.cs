using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovementState
{
    void Initialize(Playermovement player);
    void OnEnter();
    void OnUpdate();
    void OnExit();
    void Jump(InputAction.CallbackContext context);
}
