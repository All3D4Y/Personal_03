﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    CharacterFactory characterFactory;      // 캐릭터 생성용 팩토리
    CharacterUIPool characterUIPool;        // 캐릭터 UI 풀
    DamageNumberPool damageNumberPool;      // 대미지 UI 풀
    ArrowPool arrowPool;                    // 화살 풀
    ArrowHitEffectPool arrowHitEffectPool;  // 화살 이펙트 풀
    SlashEffectPool slashEffectPool;        // 베기 이펙트 풀
    MagicHitEffectPool magicHitEffectPool;  // 마법 이펙트 풀
    HealEffectPool healEffectPool;          // 회복 이펙트 풀

    // 프로퍼티
    public CharacterFactory CharacterFactory => characterFactory;
    public CharacterUIPool CharacterUIPool => characterUIPool;
    public DamageNumberPool DamageNumberPool => damageNumberPool;
    public ArrowPool ArrowPool => arrowPool;
    public ArrowHitEffectPool ArrowHitEffectPool => arrowHitEffectPool;
    public SlashEffectPool SlashEffectPool => slashEffectPool;
    public MagicHitEffectPool MagicHitEffectPool => magicHitEffectPool;
    public HealEffectPool HealEffectPool => healEffectPool;

    /// <summary>
    /// 초기화 함수
    /// </summary>
    protected override void OnInitialize()
    {
        characterFactory = transform.GetChild(0).GetComponent<CharacterFactory>();
        characterUIPool = transform.GetChild(1).GetComponent<CharacterUIPool>();
        damageNumberPool = transform.GetChild(2).GetComponent<DamageNumberPool>();
        arrowPool = transform.GetChild(3).GetComponent<ArrowPool>();
        arrowHitEffectPool = transform.GetChild(4).GetComponent<ArrowHitEffectPool>();
        slashEffectPool = transform.GetChild(5).GetComponent<SlashEffectPool>();
        magicHitEffectPool = transform.GetChild(6).GetComponent<MagicHitEffectPool>();
        healEffectPool = transform.GetChild(7).GetComponent<HealEffectPool>();

        if (damageNumberPool != null)
            damageNumberPool.Initialize();

        if (arrowPool != null)
            arrowPool.Initialize();

        if (arrowHitEffectPool != null)
            arrowHitEffectPool.Initialize();

        if (slashEffectPool != null)
            slashEffectPool.Initialize();

        if (magicHitEffectPool != null)
            magicHitEffectPool.Initialize();

        if (healEffectPool != null)
            healEffectPool.Initialize();
    }

    /// <summary>
    /// 대미지 UI를 표시하는 함수, 크리티컬 시 색상 변경
    /// </summary>
    /// <param name="position">대미지 UI가 표시될 위치</param>
    /// <param name="damageAmount">대미지 수치</param>
    /// <param name="isCritical">크리티컬 발생 여부</param>
    public void GetDamageUI(Vector2 position, float damageAmount, bool isCritical)
    {
        int intDamage = Mathf.FloorToInt(damageAmount);

        int count = Mathf.FloorToInt(Mathf.Log10(intDamage));

        int[] numbers = new int[count + 1];

        for (int i = count; i >= 0; i--)
        {
            numbers[i] = Mathf.FloorToInt(intDamage / Mathf.Pow(10, i));
            intDamage = (int)(intDamage % Mathf.Pow(10, i));
        }

        for (int i = 0; i < count + 1; i++)
        {
            Vector2 temp = position + new Vector2(0.15f + i * -0.3f, 0.3f);
            GetDamageNumber(temp, numbers[i], isCritical);
        }
    }

    /// <summary>
    /// 대미지 UI용 숫자 1개를 반환하는 함수
    /// </summary>
    /// <param name="position">반환할 위치</param>
    /// <param name="number">숫자</param>
    /// <param name="isCritical">크리티컬 여부</param>
    /// <returns></returns>
    DamageNumberUI GetDamageNumber(Vector2 position, int number, bool isCritical)
    {
        DamageNumberUI result = damageNumberPool.GetObject();
        result.SetNumber(number);
        result.transform.position = position;
        if (isCritical)
        {
            result.Critical();
        }

        return result;
    }

    /// <summary>
    /// 화살 프리팹을 가져오는 함수
    /// </summary>
    /// <param name="position">화살이 발사 될 위치</param>
    /// <param name="isRight">오른쪽으로 발사될 경우 true, 왼쪽 false</param>
    /// <returns></returns>
    public Arrow GetArrow(Vector2 position, bool isRight)
    {
        Arrow result = arrowPool.GetObject();
        result.isRight = isRight;
        result.transform.position = position + new Vector2(0, 0.3f);

        return result;
    }

    /// <summary>
    /// 화살 피격 이펙트를 가져오는 함수
    /// </summary>
    /// <param name="position">이펙트 위치</param>
    /// <param name="isRight">오른쪽인지</param>
    /// <returns></returns>
    public ArrowHitEffect GetArrowHitEffect(Vector2 position, bool isRight)
    {
        ArrowHitEffect result = arrowHitEffectPool.GetObject();
        result.transform.localScale = isRight? 0.6f * Vector3.one : new Vector3(-0.6f, 0.6f, 0.6f);
        result.transform.position = position + new Vector2(0, 0.3f);

        return result;
    }

    /// <summary>
    /// 베기 공격 이펙트를 가져오는 함수
    /// </summary>
    /// <param name="position">이펙트 위치</param>
    /// <param name="isRight">오른쪽인지</param>
    /// <returns></returns>
    public SlashEffect GetSlashHitEffect(Vector2 position, bool isRight)
    {
        SlashEffect result = slashEffectPool.GetObject();
        result.transform.localScale = isRight ? 0.3f * Vector3.one : new Vector3(-0.3f, 0.3f, 0.3f);
        result.transform.position = position + new Vector2(0, 0.3f);

        return result;
    }

    /// <summary>
    /// 마법 이펙트를 가져오는 함수
    /// </summary>
    /// <param name="position">이펙트 위치</param>
    /// <param name="isRight">오른쪽인지</param>
    /// <returns></returns>
    public MagicHitEffect GetMagicHitEffect(Vector2 position, bool isRight)
    {
        MagicHitEffect result = magicHitEffectPool.GetObject();
        result.transform.localScale = isRight ? Vector3.one : -Vector3.one;
        result.transform.position = position + new Vector2(0, 0.3f);

        return result;
    }

    /// <summary>
    /// 힐 이펙트를 가져오는 함수
    /// </summary>
    /// <param name="position">이펙트 위치</param>
    /// <returns></returns>
    public HealEffect GetHealEffect(Vector2 position)
    {
        HealEffect result = healEffectPool.GetObject();
        result.transform.position = position;

        return result;
    }
}
