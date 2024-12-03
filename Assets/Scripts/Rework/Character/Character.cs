using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("기본 정보")]
    [SerializeField] protected string characterName = "이름";
    [TextArea(2, 5)]
    [SerializeField] protected string description = "설명";
    [Space(20)]
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
    public SkillData[] skillDatas;

    float currentHP;
    float currentMP;

    public event Action onDie;
    public event Action<float> onHPChanged;
    public event Action<float> onMPChanged;

    // 프로퍼티
    public string Name => characterName;                // 캐릭터 이름
    public bool IsPlayer { get; set; }                  // 아군 여부
    public bool IsAlive { get; set; }                   // 생존 여부
    public float BaseSpeed => baseSpeed;                // 초기 속도
    public float CurrentSpeedIncrement { get; set; }    // 턴마다 증가하는 속도
    public float CurrentSpeed { get; set; }             // 현재 속도

    /// <summary>
    /// 체력
    /// </summary>
    public float HP
    {
        get => currentHP;
        set
        {
            currentHP = value;
            onHPChanged?.Invoke(currentHP / maxHp);
            if (currentHP < 0)
            {
                Die();
                onDie?.Invoke();
                Debug.Log($"{this.gameObject.name}is dead!");
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
            currentMP = value;
            onMPChanged?.Invoke(currentMP / maxMp);
            if (currentMP < 0)
            {
                OnLowMP();
            }
            else
            {
                OffLowMP();
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

    public void Initialize()
    {
        HP = maxHp;
        MP = maxMp;
        ATK = attackPower;
        DEF = defensivePower;
        CurrentSpeed = BaseSpeed;
        CurrentSpeedIncrement = speedIncrement;
    }

    void Die()
    {
    }

    void OnLowMP()
    {
        //foreach (IAttack attack in skillDatas)
        //{
        //    // 계수 * 0.2f
        //}
    }

    void OffLowMP()
    {
        //foreach (IAttack attack in skillDatas)
        //{
        //    // 계수 정상화
        //}
    }
}