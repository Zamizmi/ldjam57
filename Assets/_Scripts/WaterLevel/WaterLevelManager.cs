using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelManager : MonoBehaviour
{
    private string ANIM_LOWER_WATER_LEVEL1 = "LOWER_WATER_LEVEL1";
    private string ANIM_LOWER_WATER_LEVEL2 = "LOWER_WATER_LEVEL2";
    private string ANIM_LOWER_WATER_LEVEL3 = "LOWER_WATER_LEVEL3";
    private string ANIM_LOWER_WATER_LEVEL4 = "LOWER_WATER_LEVEL4";
    private string ANIM_LOWER_WATER_LEVEL5 = "LOWER_WATER_LEVEL5";
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InteractActions.OnLeverActivated += HandleLeverActions;

    }

    private void HandleLeverActions(List<string> list)
    {
        foreach (var item in list)
        {
            if (item == ANIM_LOWER_WATER_LEVEL1)
            {
                animator.SetBool(ANIM_LOWER_WATER_LEVEL1, true);
            }
            if (item == ANIM_LOWER_WATER_LEVEL2)
            {
                animator.SetBool(ANIM_LOWER_WATER_LEVEL2, true);
            }
            if (item == ANIM_LOWER_WATER_LEVEL3)
            {
                animator.SetBool(ANIM_LOWER_WATER_LEVEL3, true);
            }
            if (item == ANIM_LOWER_WATER_LEVEL4)
            {
                animator.SetBool(ANIM_LOWER_WATER_LEVEL4, true);
            }
            if (item == ANIM_LOWER_WATER_LEVEL5)
            {
                animator.SetBool(ANIM_LOWER_WATER_LEVEL5, true);
            }
        }
    }
}
