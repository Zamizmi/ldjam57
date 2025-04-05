using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public Action OnInteractHandler;
    public Action OnJumpHandler;
    public Action OnInteractAlternativeHandler;
    public Action OnPauseAction;

    private InputSystem_Actions playerInputActions;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputSystem_Actions();
        playerInputActions.Player.Enable();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Jump.performed += JumpPerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.performed -= JumpPerformed;
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpHandler?.Invoke();
    }

    private void OnDestroy()
    {

        playerInputActions.Dispose();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }

    public Vector2 GetLookingVector()
    {
        Vector2 inputVector = playerInputActions.Player.Look.ReadValue<Vector2>();
        return inputVector;
    }
}
