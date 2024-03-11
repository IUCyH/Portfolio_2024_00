using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMotion
{
    Idle,
    Move,
    Jump,
    OnAir,
    JumpEnd,
    DefaultAttack
}

public class PlayerAnimationController : AnimationManagement
{
    List<int> animationIDs = new List<int>();

    public PlayerAnimationController(Animator animator)
    {
        ThisAnimator = animator;
        SetAnimationIDs();
    }
    
    protected override void SetAnimationIDs()
    {
        foreach (var anim in Enum.GetValues(typeof(PlayerMotion)))
        {
            var id = Animator.StringToHash(anim.ToString());
            animationIDs.Add(id);
        }
    }

    public void Play(PlayerMotion motion)
    {
        base.PlayWithTrigger(animationIDs[(int)motion]);
    }

    public float GetAnimationPlayTime(PlayerMotion motion)
    {
        return GetAnimationPlayTime(motion.ToString());
    }
}
