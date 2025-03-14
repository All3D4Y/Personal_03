using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Skill
{
    [SerializeField] int itemID;

    public int ItemID => itemID;
    public override void Affect(Character user, Character target)
    {
    }
}
