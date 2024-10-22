using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    /// <summary>
    /// 스킬 계수
    /// </summary>
    float DamageRatio { get; }

    /// <summary>
    /// 크리티컬 확률
    /// </summary>
    float CriticalRate { get; }

    /// <summary>
    /// 크리티컬 보너스 배율
    /// </summary>
    float CriticalBonus { get; }

    /// <summary>
    /// 공격력을 받아 크리티컬과 스킬 대미지 계수를 적용한 최종 대미지를 리턴하는 함수
    /// </summary>
    /// <param name="baseATK">사용자 공격력</param>
    /// <returns>최종 대미지</returns>
    public float DoDamage(float baseATK);

    /// <summary>
    /// 공격한 대미지와 방어력을 받아 방어력을 적용한 실제 받는 대미지를 리턴하는 함수
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public float GetDamage(float damage, float def);
}
