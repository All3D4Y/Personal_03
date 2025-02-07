using System;
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
    public ItemSkill[] skillDatas;

    float currentHP;
    float currentMP;

    Vector2 cemetery = new Vector2(0, -200);

    public event Action onDie;
    public event Action<float> onHPChanged;
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

    public CharacterStatusUI CUI { get; set; }

    public CharacterAnim CharacterAnim { get; private set; }

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
            currentMP = value;
            onMPChanged?.Invoke(currentMP / maxMp);
            if (IsAlive)
            {
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

    public void Initialize()
    {
        Index = 999;
        HP = maxHp;
        MP = maxMp;
        ATK = attackPower;
        DEF = defensivePower;
        CurrentSpeed = BaseSpeed;
        CurrentSpeedIncrement = speedIncrement;
        IsAlive = true;
    }

    void Die()
    {
        IsAlive = false;
        GoToCemetery();
        onDie?.Invoke();
    }

    void OnLowMP()
    {
        foreach (IAttack attack in skillDatas)
        {
            ATK *= 0.5f;
        }
    }

    void OffLowMP()
    {
        foreach (IAttack attack in skillDatas)
        {
            ATK = attackPower;
        }
    }

    void GoToCemetery()
    {
        transform.Translate(cemetery);
    }
}