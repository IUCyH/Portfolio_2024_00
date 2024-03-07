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
}
