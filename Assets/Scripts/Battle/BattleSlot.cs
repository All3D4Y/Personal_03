using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlot
{
    EntityData entity = null;

    /// <summary>
    /// 1~4 번 자리는 플레이어블 캐릭터, 5~8 번 자리는 몬스터 or 보스
    /// </summary>
    public uint Index { get;  private set; }

    public bool IsEmpty => entity == null;


    public EntityData EntityData
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
        EntityData = null;
    }

    public void AssignSlot(EntityData entity, bool isEmpty = true)
    {
        if (entity != null)
        {
            EntityData = entity;
            isEmpty = false;
        }
        else
        {
            EntityData = null;
            isEmpty = true;
        } 
    }
}
