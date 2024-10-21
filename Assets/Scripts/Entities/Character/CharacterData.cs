using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Scripable Objects/Charater Data", order = 0)]
public class CharacterData : EntityData
{
    public Characters shiraken = Characters.Flare;
    public float ultimateConsumption = 60.0f;


    public float UltConsuption
    {
        get => ultimateConsumption; 
        set => ultimateConsumption = value;
    }

    public override void Animation()
    {
        // animation
    }

    public override void Skill()
    {
        // skill?
    }

}
