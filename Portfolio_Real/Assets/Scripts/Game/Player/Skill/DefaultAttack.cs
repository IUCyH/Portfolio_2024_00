using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAttack : MonoBehaviour, ISkill
{
    [SerializeField]
    PlayerController playerController;

    float animPlayTime;
    float playTimeTimer;

    public InputKeys SkillKey { get; } = InputKeys.DefaultAttack;
    public int ID { get; set; }
    public bool CanUse { get; set; }
    public bool EndUseSkill { get; private set; }

    void Start()
    {
        animPlayTime = playerController.GetAnimationPlayTime(PlayerMotion.DefaultAttack);
        StartCoroutine(Coroutine_Update());
        Debug.Log(animPlayTime);
    }

    IEnumerator Coroutine_Update()
    {
        while (true)
        {
            if (!EndUseSkill)
            {
                playTimeTimer += Time.deltaTime;
                if (Mathf.Approximately(animPlayTime, playTimeTimer) || animPlayTime < playTimeTimer)
                {
                    EndUseSkill = true;
                    playTimeTimer = 0f;
                    playerController.DeleteSkillStateFlagBit(PlayerState.DefaultAttack);
                }
            }
            
            yield return null;
        }
    }

    public void Execute()
    {
        playerController.ChangeStateAndPlayAnimation(PlayerState.DefaultAttack);
        playerController.SetSkillStateFlagBit(PlayerState.DefaultAttack);
        EndUseSkill = false;
    }

    public bool CalculateCoolDown()
    {
        return true; //기본공격은 쿨다운이 없으므로 무조건 true 반환
    }
}
