using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManagement
{
    Animator animator;
    int prevAnim;
    
    protected Animator ThisAnimator { set => animator = value; }
    
    protected abstract void SetAnimationIDs();
    
    protected void PlayWithTrigger(int animID)
    {
        animator.ResetTrigger(prevAnim);
        animator.SetTrigger(animID);

        prevAnim = animID;
    }

    protected float GetAnimationPlayTime(string animName)
    {
        var runtimeAnimCtr = animator.runtimeAnimatorController;
        var length = runtimeAnimCtr.animationClips.Length;
        var playTime = 0f;
        
        for (int i = 0; i < length; i++)
        {
            if (runtimeAnimCtr.animationClips[i].name == animName)
            {
                playTime = runtimeAnimCtr.animationClips[i].length;
            }
        }

        return playTime;
    }
}
