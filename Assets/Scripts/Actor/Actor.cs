using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : RecycleObject
{
    [Header("기본 정보")]
    [SerializeField] protected ActorCode code;
    [SerializeField] protected ActorSide side;
    [SerializeField] protected ActorType actorType;
    [SerializeField] protected string actorName = "이름";
    [TextArea(2,5)]
    [SerializeField] protected string description = "설명";
    [Space(20)]
    [SerializeField] protected float attackPower = 0f;
    [SerializeField] protected float defensivePower = 0f;
    [SerializeField] protected float maxHp = 0f;
    [SerializeField] protected float maxMp = 0f;
    [Space(20)]
    [Header("턴 배정 속도")]
    [SerializeField] protected float initialSpeed = 0f;     // 초기 속도
    [SerializeField] protected float increasingSpeed = 0;   // 턴이 지날 때마다 속도가 증가하는 양
    [Space(20)]
    [Header("보유 스킬")]
    public SkillData[] skillDatas;

    
    public event Action onDie;
    public event Action<float> onHPChanged;
    public event Action<float> onMPChanged;

    protected float currentHp;
    protected float currentMp;
    protected float currentSpeed;
    protected float currentATK;
    protected float currentDEF;
    protected float currentIncreasingSpeed;

    protected SPUM_Prefabs spum;
    protected bool isAttackEnd = false;
    protected bool isHurtEnd = false;

    // Properties

    public bool IsAttackEnd { get => isAttackEnd; set => isAttackEnd = value; }
    public bool IsHurtEnd { get => isHurtEnd; set => isHurtEnd = value; }

    public string ActorName => actorName;
    public ActorCode Code => code;
    public ActorSide Side => side;
    public ActorType Type => actorType;

    public float MaxHP => maxHp;
    public float MaxMP => maxMp;

    public SPUM_Prefabs SPUM => spum;

    /// <summary>
    /// 체력
    /// </summary>
    public float HP
    {
        get => currentHp;
        set
        {
            currentHp = value;
            onHPChanged?.Invoke(currentHp / maxHp);
            if (currentHp < 0)
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
        get => currentMp;
        set
        {
            currentMp = value;
            onMPChanged?.Invoke(currentMp / maxMp);
            if (currentMp < 0)
            {
                LowMpMode();
            }
        }

    }

    /// <summary>
    /// 공격력
    /// </summary>
    public float ATK
    {
        get => currentATK;
        set => currentATK = value;
    }

    /// <summary>
    /// 방어력
    /// </summary>
    public float DEF
    {
        get => currentDEF;
        set => currentDEF = value;
    }

    /// <summary>
    /// 현재 속도
    /// </summary>
    public float Speed
    {
        get => currentSpeed;
        set => currentSpeed = value;
    }

    /// <summary>
    /// 턴마다 속도가 증가하는 양 (버프나 아이템으로 증가 가능)
    /// </summary>
    public float IncreasingSpeed
    {
        get => currentIncreasingSpeed;
        set => currentIncreasingSpeed = value;
    }

    void Awake()
    {
        spum = GetComponent<SPUM_Prefabs>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        HP = maxHp;
        MP = maxMp;
        ATK = attackPower;
        DEF = defensivePower;
        Speed = initialSpeed;
        IncreasingSpeed = increasingSpeed;
    }

    public virtual void AttackAnimation(int num)
    {
        // 애니메이션
    }

    public void HurtAnimation()
    {
        spum.PlayAnimation(4);
    }

    public void Die()
    {
        // 죽음
    }

    public void LowMpMode()
    {
        // MP가 0 이하일때
    }

    public void HurtAnimationEnd()
    {
        IsHurtEnd = true;
    }
}
