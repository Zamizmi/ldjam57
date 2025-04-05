using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public Action OnInteractHandler;
    public Action OnJumpHandler;
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
        playerInputActions.Player.Interact.started += InteractStarted;
        playerInputActions.Player.Interact.performed += InteractPerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.performed -= JumpPerformed;
        playerInputActions.Player.Interact.performed -= InteractPerformed;
        playerInputActions.Player.Interact.started -= InteractStarted;
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpHandler?.Invoke();
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractHandler?.Invoke();
    }

    private void InteractStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractHandler?.Invoke();
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
