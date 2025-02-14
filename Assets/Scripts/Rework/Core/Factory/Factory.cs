using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    CharacterFactory characterFactory;
    CharacterUIPool characterUIPool;
    DamageNumberPool damageNumberPool;
    ArrowPool arrowPool;
    ArrowHitEffectPool arrowHitEffectPool;
    SlashEffectPool slashEffectPool;
    MagicHitEffectPool magicHitEffectPool;

    public CharacterFactory CharacterFactory => characterFactory;
    public CharacterUIPool CharacterUIPool => characterUIPool;
    public DamageNumberPool DamageNumberPool => damageNumberPool;
    public ArrowPool ArrowPool => arrowPool;
    public ArrowHitEffectPool ArrowHitEffectPool => arrowHitEffectPool;
    public SlashEffectPool SlashEffectPool => slashEffectPool;
    public MagicHitEffectPool MagicHitEffectPool => magicHitEffectPool;
    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        characterFactory = transform.GetChild(0).GetComponent<CharacterFactory>();
        characterUIPool = transform.GetChild(1).GetComponent<CharacterUIPool>();
        damageNumberPool = transform.GetChild(2).GetComponent<DamageNumberPool>();
        arrowPool = transform.GetChild(3).GetComponent<ArrowPool>();
        arrowHitEffectPool = transform.GetChild(4).GetComponent<ArrowHitEffectPool>();
        slashEffectPool = transform.GetChild(5).GetComponent<SlashEffectPool>();
        magicHitEffectPool = transform.GetChild(6).GetComponent<MagicHitEffectPool>();
    }

    protected override void OnInitialize()
    {
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
    }

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

    public DamageNumberUI GetDamageNumber(Vector2 position, int number, bool isCritical)
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

    public Arrow GetArrow(Vector2 position, bool isRight)
    {
        Arrow result = arrowPool.GetObject();
        result.isRight = isRight;
        result.transform.position = position + new Vector2(0, 0.3f);

        return result;
    }

    public ArrowHitEffect GetArrowHitEffect(Vector2 position, bool isRight)
    {
        ArrowHitEffect result = arrowHitEffectPool.GetObject();
        result.transform.localScale = isRight? 0.6f * Vector3.one : new Vector3(-0.6f, 0.6f, 0.6f);
        result.transform.position = position + new Vector2(0, 0.3f);

        return result;
    }

    public SlashEffect GetSlashHitEffect(Vector2 position, bool isRight)
    {
        SlashEffect result = slashEffectPool.GetObject();
        result.transform.localScale = isRight ? 0.3f * Vector3.one : new Vector3(-0.3f, 0.3f, 0.3f);
        result.transform.position = position + new Vector2(0, 0.3f);

        return result;
    }

    public MagicHitEffect GetMagicHitEffect(Vector2 position, bool isRight)
    {
        MagicHitEffect result = magicHitEffectPool.GetObject();
        result.transform.localScale = isRight ? Vector3.one : -Vector3.one;
        result.transform.position = position + new Vector2(0, 0.3f);

        return result;
    }
}
