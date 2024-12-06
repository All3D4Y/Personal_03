using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New SKill_Buff Data", menuName = "Scripable Objects/SKill_Buff Data", order = 3)]
public class OldSkill_Buff : SkillData, IOldBuffDebuff
{
    public BuffDebuffType buffDebufftype;

    public float ratio;

    public int duration;

    public BuffDebuffType BuffDebuffType => buffDebufftype;

    public float BuffRatio => ratio;

    public int Duration
    {
        get => duration;
        set
        {
            duration = value;
            if (duration < 0)
            {
                Undo();
            }
        }
    }

    float tempValue;

    public override void ActionExecute(BattleSlot user, BattleSlot[] targets)
    {
        BuffDebuff(BuffDebuffType, targets);
        BuffDebuffContainer.SaveBuff(this);
    }

    public void BuffDebuff(BuffDebuffType type, BattleSlot[] targets)
    {
        if (Duration > 0)
        {
            switch (type)
            {
                case BuffDebuffType.Attack:
                    foreach (var target in targets)
                    {
                        tempValue = target.ActorData.ATK;
                        target.ActorData.ATK *= 1 + ratio;
                    }
                    break;
                case BuffDebuffType.Defense:
                    foreach (var target in targets)
                    {
                        tempValue = target.ActorData.DEF;
                        target.ActorData.DEF *= 1 + ratio;
                    }
                    break;
                case BuffDebuffType.Speed:
                    foreach (var target in targets)
                    {
                        tempValue = target.ActorData.Speed;
                        target.ActorData.Speed *= 1 + ratio;
                    }
                    break;
            }
        }
    }

    public void Do()
    {
        BattleSlot user = OldGameManager.Instance.BattleManager.OnTurnSlot;

        BattleSlot[] targets = SetTarget(user, affectType);

        BuffDebuff(BuffDebuffType, targets);
        Debug.Log("버프 적용");
    }

    public void Undo()  
    {
        BattleSlot user = OldGameManager.Instance.BattleManager.OnTurnSlot;

        BattleSlot[] targets = SetTarget(user, affectType);

        switch (BuffDebuffType)
        {
            case BuffDebuffType.Attack:
                foreach (var target in targets)
                {
                    target.ActorData.ATK = tempValue;
                }
                break;
            case BuffDebuffType.Defense:
                foreach (var target in targets)
                {
                    target.ActorData.DEF = tempValue;
                }
                break;
            case BuffDebuffType.Speed:
                foreach (var target in targets)
                {
                    target.ActorData.Speed = tempValue;
                }
                break;
        }

        Debug.Log("버프 해제");
    }
}
