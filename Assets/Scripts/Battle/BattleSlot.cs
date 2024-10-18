using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlot
{
    EntityData entityData = null;

    public EntityType Type {  get; private set; }
    public uint Index { get;  private set; }

    public bool IsEmpty => entityData == null;



    public EntityData EntityData
    {
        get => entityData;
        private set
        {
            if (entityData != value)
                entityData = value;
        }
    }

    public BattleSlot(EntityType type, uint index)
    {
        Type = type;
        Index = index;
        EntityData = null;
    }

    /// <summary>
    /// 슬롯에 Entity를 넣는 함수
    /// </summary>
    /// <param name="entityData">넣을 Entity의 정보</param>
    public void AssignSlot(EntityData entityData)
    {
        if (entityData != null)
        {
            EntityData = entityData;
        }
        else
        {
            EntityData = null;
        } 
    }
}
