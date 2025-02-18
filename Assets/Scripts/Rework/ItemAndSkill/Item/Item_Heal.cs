using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item_Heal Data", menuName = "Scripable Objects/Item_Heal Data", order = 4)]
public class Item_Heal : Item, IHeal
{
    [SerializeField] HealType healType;
    [SerializeField] float healAmount;
    public HealType Type => healType;

    public float Amount => healAmount;

    public override void Affect(Character user, Character target)
    {
        if (healType == HealType.HP)
            target.HP += healAmount;
        else
            target.MP += healAmount;
    }
}
