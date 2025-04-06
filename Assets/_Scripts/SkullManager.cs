using System;
using System.Collections.Generic;
using UnityEngine;

public class SkullManager : MonoBehaviour
{
    [SerializeField] private List<DreamTarget> targetsToActivate;
    [SerializeField] private List<string> eventStringsToSend;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isCompleted = false;
    private string ANIM_COMPLETED = "SkullCompleted";


    // Update is called once per frame
    void Update()
    {
        CheckTargetsAreCompleted();
    }

    private void CheckTargetsAreCompleted()
    {
        if (isCompleted) return;
        foreach (var target in targetsToActivate)
        {
            if (!target.IsActivated()) return;
        }
        InteractActions.OnLevelPulled(eventStringsToSend);
        animator.SetBool(ANIM_COMPLETED, isCompleted);
        isCompleted = true;
    }
}
