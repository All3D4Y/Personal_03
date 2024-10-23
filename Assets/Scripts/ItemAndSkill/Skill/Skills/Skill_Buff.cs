using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SKill_Buff Data", menuName = "Scripable Objects/SKill_Buff Data", order = 5)]
public class Skill_Buff : SkillData, IBuffDebuff
{
    public BuffDebuffType buffDebufftype;

    public float ratio;

    public int duration;

    public BuffDebuffType BuffDebuffType => buffDebufftype;

    public float BuffRatio => ratio;

    public int Duration => duration;

    float tempValue;

    public override void ActionExecute(BattleSlot user, BattleSlot[] targets)
    {
        BuffDebuffInvoker.DoBuffDebuff(this);
    }

    public void BuffDebuff(BuffDebuffType type, BattleSlot[] targets)
    {
        switch (type)
        {
            case BuffDebuffType.Attack:
                foreach (var target in targets)
                {
                    tempValue = target.EntityData.ATK;
                    target.EntityData.ATK *= 1 + ratio;
                }
                break;
            case BuffDebuffType.Defense:
                foreach (var target in targets)
                {
                    tempValue = target.EntityData.DEF;
                    target.EntityData.DEF *= 1 + ratio;
                }
                break;
            case BuffDebuffType.Speed:
                foreach (var target in targets)
                {
                    tempValue = target.EntityData.Speed;
                    target.EntityData.Speed *= 1 + ratio;
                }
                break;
        }
    }

    public void Do()
    {
        BattleSlot user = GameManager.Instance.BattleManager.OnTurnSlot;

        BattleSlot[] targets = SetTarget(user, affectType);

        BuffDebuff(BuffDebuffType, targets);
        Debug.Log("버프 적용");
    }

    public void Undo()
    {
        BattleSlot user = GameManager.Instance.BattleManager.OnTurnSlot;

        BattleSlot[] targets = SetTarget(user, affectType);

        switch (BuffDebuffType)
        {
            case BuffDebuffType.Attack:
                foreach (var target in targets)
                {
                    target.EntityData.ATK = tempValue;
                }
                break;
            case BuffDebuffType.Defense:
                foreach (var target in targets)
                {
                    target.EntityData.DEF = tempValue;
                }
                break;
            case BuffDebuffType.Speed:
                foreach (var target in targets)
                {
                    target.EntityData.Speed = tempValue;
                }
                break;
        }

        Debug.Log("버프 해제");
    }
}
