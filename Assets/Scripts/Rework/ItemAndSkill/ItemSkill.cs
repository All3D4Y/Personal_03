using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSkill : ScriptableObject
{
    public Sprite icon;
    public string iS_name;
    [TextArea(2, 5)]
    [SerializeField] protected string iS_description;
    [SerializeField] protected int count = 1;
    [SerializeField] protected int range = 1;

    public int Count => count;
    public int Range => range;

    public abstract void Affect(Character character);
    public abstract void SetTarget();
}
