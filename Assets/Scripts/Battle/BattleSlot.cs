using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlot
{
    /// <summary>
    /// 1~4 번 자리는 플레이어블 캐릭터, 5~8 번 자리는 몬스터 or 보스
    /// </summary>
    public uint Index { get;  private set; }

    EntityData entity = null;

    public EntityData Entity
    {
        get => entity;
        private set
        {
            if (entity != value)
                entity = value;
        }
    }

    public BattleSlot(uint index)
    {
        Index = index;
        Entity = null;
    }

    public void AssignSlot(EntityData entity)
    {
        if (entity != null)
            Entity = entity;
        else 
            Entity = null;
    }
}
