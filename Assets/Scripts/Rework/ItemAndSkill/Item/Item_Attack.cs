using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item_Attack Data", menuName = "Scripable Objects/Item_Attack Data", order = 3)]
public class Item_Attack : Item, IAttack
{
    [SerializeField] float damage = 10;

    float ratio = 0;
    float criticalRate = 0;
    float criticalBonus = 0;

    public float Ratio => ratio;

    public float CriticalRate => criticalRate;

    public float CriticalBonus => criticalBonus;

    public override void Affect(Character user, Character target)
    {
        if (target != null)
        {
            Factory.Instance.GetDamageUI(target.transform.position, damage, false);
            target.HP -= damage; 
        }
    }

    public float DoDamage(Character user, out bool isCritical)
    {
        throw new System.NotImplementedException();
    }
}
