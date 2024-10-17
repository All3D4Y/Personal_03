using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataManager : EntityDataManager
{
    public CharacterData[] characters;
    public override EntityData this[uint index] 
    { 
        get => characters[index];
        set => characters[index] = value as CharacterData; 
    }
}
