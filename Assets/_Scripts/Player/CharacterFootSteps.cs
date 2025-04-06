using UnityEngine;

public class CharacterFootSteps : MonoBehaviour
{
    [SerializeField] private IHasFootSteps hasFootSteps;
    private float footstepTimer;
    [SerializeField] private float footstepTimerMax = .1f;

    private void Start()
    {
        hasFootSteps = GetComponent<IHasFootSteps>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (hasFootSteps.IsWalking())
            {
                PlayerEvents.RaiseOnFootStep(transform.position);
            }
        }
    }
}
