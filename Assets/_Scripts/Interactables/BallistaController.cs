using Unity.Cinemachine;
using UnityEngine;

public class BallistaController : MonoBehaviour, IInteractable
{
    public Transform aimPivot;
    public Transform launchPoint;
    public DreamArrow projectilePrefab;
    public float rotationSpeed = 30f;
    public float launchForce = 50f;
    public float mouseSensitivity = 1f;
    public float shootingCooldown = 2f;
    private float cooldownTimer = 0f;
    public CinemachineCamera ballistCamera;
    public bool isAiming = false;
    // This is used as a fail safe to get free from ballista
    private Player activatedPlayer;

    public void Interact(Player player)
    {
        if (isAiming)
        {
            ballistCamera.gameObject.SetActive(false);
            player.EnableLooking();
        }
        else
        {
            activatedPlayer = player;
            ballistCamera.gameObject.SetActive(true);
            player.DisableLooking();
        }
        isAiming = !isAiming;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (!isAiming) return;

        HandleAiming();

        if (Input.GetMouseButtonDown(0)) // Left click to shoot
            Shoot();
    }

    void HandleAiming()
    {
        Vector2 lookInput = GameInput.Instance.GetMovementVectorNormalized();
        if (lookInput == Vector2.zero)
        {
            lookInput = GameInput.Instance.GetLookingVector();
        }
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Rotate horizontally (yaw)
        aimPivot.Rotate(Vector3.up, mouseX, Space.World);

        // Rotate vertically (pitch)
        aimPivot.Rotate(Vector3.right, -mouseY, Space.Self);
    }

    void Shoot()
    {
        if (cooldownTimer <= shootingCooldown) return;
        InteractableEvents.RaiseOnBallistaShot(launchPoint.position);
        cooldownTimer = 0f;
        DreamArrow projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(launchPoint.forward * launchForce);
        }
    }

    public string GetInteractText(Player actor)
    {
        if (actor != activatedPlayer)
        {
            return "Use Cannon";
        }
        else
        {
            return "leave Cannon";
        }
    }
}
