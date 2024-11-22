using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlot
{
    Actor actorData = null;

    DamagePasser passer = null;

    uint index = 9999;

    /// <summary>
    /// 어느 편인지 확인하는 프로퍼티
    /// </summary>
    public ActorSide Side {  get; private set; }

    /// <summary>
    /// 슬롯의 인덱스를 확인하는 프로퍼티
    /// </summary>
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

    /// <summary>
    /// 슬롯이 비어있는지 확인하는 프로퍼티 (true면 비어있다)
    /// </summary>
    public bool IsEmpty => actorData == null;

    /// <summary>
    /// 슬롯에 들어있는 Actor의 데이터를 확인하는 프로퍼티
    /// </summary>
    public Actor ActorData
    {
        get => actorData;
        private set
        {
            if (actorData != value)
            {
                actorData = value;
            }
        }
    }

    public DamagePasser Passer
    {
        get => passer;
        set => passer = value;
    }

    /// <summary>
    /// BattleSlot 생성자
    /// </summary>
    /// <param name="side">어느 편</param>
    /// <param name="index">인덱스</param>
    public BattleSlot(ActorSide side, uint index)
    {
        Side = side;
        Index = index;
        ActorData = null;
    }

    /// <summary>
    /// 이 슬롯에 Actor를 배치하는 함수
    /// </summary>
    /// <param name="actorData">넣을 Actor의 정보</param>
    public void AssignData(Actor actorData)
    {
        if (actorData != null)
        {
            ActorData = actorData;
        }
        else
        {
            ActorData = null;
        } 
    }

    /// <summary>
    /// 이 슬롯을 비우는 함수
    /// </summary>
    public void ClearData()
    { 
        ActorData = null; 
    }
}
