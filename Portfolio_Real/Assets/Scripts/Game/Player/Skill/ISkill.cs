using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    InputKeys SkillKey { get; }
    int ID { get; set; }
    bool CanUse { get; set; }
    bool EndUseSkill { get; }

    void Execute();
    bool CalculateCoolDown();
}
