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
        if (damageNumberPool != null)
        {
            damageNumberPool.Initialize();
        }
    }

    public DamageNumberUI[] GetDamageUI(Vector2 position, float damageAmount)
    {
        int intDamage = Mathf.FloorToInt(damageAmount);

        int count = Mathf.FloorToInt(Mathf.Log10(intDamage));

        int[] numbers = new int[count + 1];

        for (int i = count; i >= 0; i--)
        {
            numbers[i] = Mathf.FloorToInt(intDamage / Mathf.Pow(10, i));
            intDamage = (int)(intDamage % Mathf.Pow(10, i));
        }

        DamageNumberUI[] result = new DamageNumberUI[count + 1];

        for (int i = 0; i < result.Length; i++)
        {
            Vector2 temp = position + new Vector2(i * -0.6f, 0);
            result[i] = GetDamageNumber(temp, numbers[i]);
        }

        return result;
    }

    public DamageNumberUI GetDamageNumber(Vector2 position, int number)
    {
        DamageNumberUI result = damageNumberPool.GetObject(position);
        result.SetNumber(number);
        result.transform.position = position;

        return result;
    }
}
