using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    const int NoUsingSkill = -1;
    
    ISkill[] skills;
    int skillIDWillExecute;
    bool executeSkill;

    void Start()
    {
        skillIDWillExecute = NoUsingSkill;
        skills = GetComponentsInChildren<ISkill>();
        InitSKills();
    }

    public void CheckAnySkillKeyDown()
    {
        bool cannotGetInput = skillIDWillExecute != NoUsingSkill && !skills[skillIDWillExecute].EndUseSkill;
        if (cannotGetInput) return;
        
        skillIDWillExecute = NoUsingSkill;
        foreach (var skill in skills)
        {
            if (InputManager.GetKeyDown(skill.SkillKey) && skill.CanUse)
            {
                skill.CanUse = false;
                
                skillIDWillExecute = skill.ID;
                executeSkill = true;
                
                break;
            }
        }
    }

    public void ExecuteSkill()
    {
        if (!executeSkill) return;
        
        skills[skillIDWillExecute].Execute();
        executeSkill = false;
    }

    public void CalculateSKillCoolDowns()
    {
        foreach (var skill in skills)
        {
            if (!skill.CanUse)
            {
                var isCoolDownTimerOver = skill.CalculateCoolDown();
                if (isCoolDownTimerOver)
                {
                    skill.CanUse = true;
                }
            }
        }
    }

    void InitSKills()
    {
        int id = 0;
        
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].ID = id++;
            skills[i].CanUse = true;
        }
    }
}
