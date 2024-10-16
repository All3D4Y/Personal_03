using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Charater = 0,
    Enemy
}

public abstract class EntityData : ScriptableObject
{
    [Header("기본 정보")]
    public GameObject prefab;       // 나중에 삭제
    [Space(20)]
    [SerializeField] protected float attackPower = 10.0f;
    [SerializeField] protected float defensivePower = 5.0f;
    [SerializeField] protected float hp = 200.0f;
    [Space(20)]
    [SerializeField] protected float speed = 40.0f;
    [SerializeField] protected float initialSpeed = 60.0f;

    // Properties
    public float ATK
    {
        get => attackPower;
        set => attackPower = value;
    }

    public float DEF
    {
        get => defensivePower;
        set => defensivePower = value;
    }

    public float HP
    {
        get => hp;
        set => hp = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float InitialSpeed
    {
        get => initialSpeed;
        set => initialSpeed = value;
    }


    public abstract void Animation();

    public abstract void Skill();
}
