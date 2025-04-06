using UnityEngine;

public class DreamTarget : MonoBehaviour
{
    [SerializeField] private Material colorAfterHit;
    [SerializeField] private MeshRenderer rendererToChangeColor;
    private bool isActivated;

    public void TryToActivate()
    {
        if (isActivated) return;
        isActivated = true;
        rendererToChangeColor.material = colorAfterHit;
        InteractableEvents.RaiseOnTargetActivation(transform.position);
    }

    public bool IsActivated()
    {
        return isActivated;
    }
}
