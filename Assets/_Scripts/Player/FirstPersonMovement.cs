using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour, IHasFootSteps
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    [SerializeField] private float groundCheckDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    [Header("Looking")]
    [SerializeField] private float mouseSensitivity = 50f;
    [SerializeField] private Transform virtualCamera;
    private Vector2 lookInput;
    private float xRotation = 0f;
    private bool movementAllowed;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementAllowed = true;
    }

    private void Update()
    {
        HandleMouseLooking();
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        if (!movementAllowed) return;
        GroundCheck();

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        isWalking = inputVector != Vector2.zero && isGrounded;
        Vector3 moveDir = transform.right * inputVector.x + transform.forward * inputVector.y;

        // Reset vertical velocity if grounded and not jumping
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // small value keeps grounded

        // Apply gravity
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // Move the character
        Vector3 finalMove = moveDir * moveSpeed + velocity;
        characterController.Move(finalMove * Time.deltaTime);
    }

    public void DisableMovement()
    {
        movementAllowed = false;
        characterController.enabled = false;
    }

    public void EnableMovement()
    {
        movementAllowed = true;
        characterController.enabled = true;
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void HandleMouseLooking()
    {
        if (!movementAllowed) return;
        // Apply sensitivity and deltaTime to input
        lookInput = gameInput.GetLookingVector();
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Rotate the player (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the Cinemachine Virtual Camera (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        virtualCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * Physics.gravity.y);
            PlayerEvents.RaiseOnFootJump(transform.position);
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckDistance, groundMask);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
