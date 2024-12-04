using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : RecycleObject
{
    Animator animator;

    readonly int Count_Hash = Animator.StringToHash("Count");

    public SkillData Skill {  get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AssignSkill(SkillData skillData)
    {
        Skill = skillData;
        animator.SetInteger(Count_Hash, (int)skillData.EffectCount);
    }

    public void ClearSkill()
    {
        Skill = null;
    }

    public void Initialize()
    {
        if (Skill == null)
        {
            Debug.LogWarning("이 보조선에는 할당된 스킬이 없어 표시할 수 없습니다!");
            return;
        }
        else
        {
            // 위치 초기화시키기 로직
        }
    }
}
