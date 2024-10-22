using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityData : ScriptableObject
{
    [Header("기본 정보")]
    public string entityName = "이름";
    public GameObject prefab;       // 나중에 삭제
    [Space(20)]
    public float attackPower = 10.0f;
    public float defensivePower = 5.0f;
    public float hp = 200.0f;
    [Space(20)]
    public float speed = 40.0f;
    public float initialSpeed = 60.0f;

    public SkillData[] skillDatas;

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

    public float InitialSpeed => initialSpeed;


    public abstract void Animation();

    public abstract void Skill();
}
