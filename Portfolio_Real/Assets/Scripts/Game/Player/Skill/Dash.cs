using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, ISkill
{
    [SerializeField]
    Transform targetPlayer;
    
    Vector3 targetPos;
    [SerializeField]
    float coolDown;
    float coolDownTimer;
    [SerializeField]
    float maxDashDistance;
    [SerializeField]
    float speed;
    bool dash;

    public InputKeys SkillKey { get; } = InputKeys.Dash;
    public int ID { get; set; }
    public bool CanUse { get; set; }
    public bool EndUseSkill { get; private set; }

    void Start()
    {
        StartCoroutine(Coroutine_Update());
    }

    IEnumerator Coroutine_Update()
    {
        while (true)
        {
            if (dash)
            {
                targetPos.y = targetPlayer.position.y;
                targetPos.z = targetPlayer.position.z;
                
                var nextPos = Vector3.MoveTowards(targetPlayer.position, targetPos, speed * Time.deltaTime);
                targetPlayer.position = nextPos;

                var currentPosX = targetPlayer.position.x * targetPlayer.localScale.x;
                var targetPosX = targetPos.x * targetPlayer.localScale.x;
                if (currentPosX >= targetPosX)
                {
                    dash = false;
                    EndUseSkill = true;
                }
            }

            yield return null;
        }
    }

    public void Execute()
    {
        var targetX = targetPlayer.position.x + (maxDashDistance * targetPlayer.localScale.x);

        targetPos.x = targetX;
        dash = true;
    }

    public bool CalculateCoolDown()
    {
        coolDownTimer += Time.deltaTime;
        if (Mathf.Approximately(coolDownTimer, coolDown) || coolDownTimer > coolDown)
        {
            coolDownTimer = 0f;

            return true;
        }

        return false;
    }
}
