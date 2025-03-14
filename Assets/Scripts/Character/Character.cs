﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("기본 정보")]
    [SerializeField] protected int code = 0;
    public string characterName = "이름";
    [TextArea(2, 5)]
    public string description = "설명";
    [Space(20)]
    [SerializeField] protected bool isPlayer = true;
    [SerializeField] protected float attackPower = 0f;
    [SerializeField] protected float defensivePower = 0f;
    [SerializeField] protected float maxHp = 0f;
    [SerializeField] protected float maxMp = 0f;
    [Space(20)]
    [Header("턴 배정 속도")]
    [SerializeField] protected float baseSpeed = 0f;        // 초기 속도
    [SerializeField] protected float speedIncrement = 0;    // 턴마다 증가하는 속도
    [Space(20)]
    [Header("보유 스킬")]
    public Skill[] skillDatas;

    float currentHP;
    float currentMP;

    Vector2 cemetery = new Vector2(0, -200);

    /// <summary>
    /// 사망을 알리는 델리게이트
    /// </summary>
    public event Action onDie;

    /// <summary>
    /// 체력에 변화가 있을 때 비율을 전달하는 델리게이트 (UI용)
    /// </summary>
    public event Action<float> onHPChanged;

    /// <summary>
    /// 마인드에 변화가 있을 때 비율을 전달하는 델리게이트 (UI용)
    /// </summary>
    public event Action<float> onMPChanged;

    // 프로퍼티
    public int Code => code;                            // 캐릭터 코드
    public string Name => characterName;                // 캐릭터 이름
    public bool IsPlayer => isPlayer;                   // 아군 여부
    public bool IsAlive { get; set; }                   // 생존 여부
    public float BaseSpeed => baseSpeed;                // 초기 속도
    public float CurrentSpeedIncrement { get; set; }    // 턴마다 증가하는 속도
    public float CurrentSpeed { get; set; }             // 현재 속도
    public int Index {  get; set; }
    public float MaxHp => maxHp;
    public float MaxMp => maxMp;

    public int Level { get; set; }
    public CharacterStatusUI CUI { get; set; }
    public BuffManager BuffManager { get; set; }

    public CharacterAnim CharacterAnim { get; private set; }

    /// <summary>
    /// 체력
    /// </summary>
    public float HP
    {
        get => currentHP;
        set
        {
            if (value <= maxHp)
                currentHP = value;
            else
                currentHP = maxHp;

            onHPChanged?.Invoke(currentHP / maxHp);

            if (currentHP < 0 && IsAlive)
            {
                Die();
                Debug.Log($"{this.gameObject.name}가 사망했습니다!");
            }
        }
    }

    /// <summary>
    /// 마인드 포인트
    /// </summary>
    public float MP
    {
        get => currentMP;
        set
        {
            if ( value <= maxMp)
                currentMP = value;
            else
                currentMP = maxMp;

            onMPChanged?.Invoke(currentMP / maxMp);

            if (IsAlive)
            {
                if (currentMP < 0)
                {
                    currentMP = 0;
                } 
            }
        }

    }

    /// <summary>
    /// 공격력
    /// </summary>
    public float ATK { get; set; }

    /// <summary>
    /// 방어력
    /// </summary>
    public float DEF { get; set; }

    void Awake()
    {
        CharacterAnim = transform.GetChild(0).GetComponent<CharacterAnim>();
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Initialize()
    {
        Index = 999;
        HP = maxHp * (1 + 0.3f *  Level);
        MP = maxMp * (1 + 0.3f * Level);
        ATK = attackPower * (1 + 0.3f * Level);
        DEF = defensivePower * (1 + 0.3f * Level);
        CurrentSpeed = BaseSpeed;
        CurrentSpeedIncrement = speedIncrement;
        IsAlive = true;

        BuffManager = new BuffManager(this);
    }

    /// <summary>
    /// 버프 비활성화
    /// </summary>
    public void BuffOff()
    {
        ATK = attackPower;
        DEF = defensivePower;
        CurrentSpeedIncrement = speedIncrement;
    }

    /// <summary>
    /// 사망 시 처리할 내용을 담은 함수
    /// </summary>
    void Die()
    {
        IsAlive = false;
        GoToCemetery();
        onDie?.Invoke();
    }

    /// <summary>
    /// 사망 시 묘지 좌표로 이동, 전투 종료 시 일괄 파괴
    /// </summary>
    void GoToCemetery()
    {
        transform.Translate(cemetery);
    }
}