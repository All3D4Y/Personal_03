using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataManager : EntityDataManager
{
    CharacterData[] characters;
    public override EntityData this[uint index] 
    { 
        get => characters[index];
        set => characters[index] = value as CharacterData; 
    }
}
