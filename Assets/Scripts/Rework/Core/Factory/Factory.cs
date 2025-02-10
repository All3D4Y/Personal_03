using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    CharacterFactory characterFactory;
    CharacterUIPool characterUIPool;
    DamageNumberPool damageNumberPool;

    public CharacterFactory CharacterFactory => characterFactory;
    public CharacterUIPool CharacterUIPool => characterUIPool;
    public DamageNumberPool DamageNumberPool => damageNumberPool;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        characterFactory = transform.GetChild(0).GetComponent<CharacterFactory>();
        characterUIPool = transform.GetChild(1).GetComponent<CharacterUIPool>();
        damageNumberPool = transform.GetChild(2).GetComponent<DamageNumberPool>();
    }

    protected override void OnInitialize()
    {
        if (damageNumberPool == null)
        {
            damageNumberPool = transform.GetChild(2).GetComponent<DamageNumberPool>();
            damageNumberPool.Initialize();
        }
        else
            damageNumberPool.Initialize();
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
}
