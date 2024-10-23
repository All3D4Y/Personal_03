using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlot
{
    EntityData entityData = null;

    uint index = 9999;

    public EntityType Type {  get; private set; }
    public uint Index 
    {
        get => index;
        set
        {
            if (index == 9999)  // 맨 처음 1번만 설정 가능
            {
                index = (uint)Mathf.Clamp(value, 0, 3);
            }
        }
    }

    public bool IsEmpty => entityData == null;



    public EntityData EntityData
    {
        get => entityData;
        private set
        {
            if (entityData != value)
            {
                if (value == null)
                {
                    entityData.onDie -= ClearData;
                }
                entityData = value;
            }
        }
    }

    /// <summary>
    /// BattleSlot 생성자
    /// </summary>
    /// <param name="type">타입</param>
    /// <param name="index">인덱스</param>
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
    public void AssignData(EntityData entityData)
    {
        if (entityData != null)
        {
            EntityData = entityData;
            entityData.onDie += ClearData;  // 할당된 entity가 죽었을 때 자동으로 데이터 삭제
        }
        else
        {
            EntityData = null;
        } 
    }

    public void ClearData()
    { 
        EntityData = null; 
    }
}
