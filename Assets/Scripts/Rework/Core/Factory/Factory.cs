using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    CharacterFactory characterFactory;

    public CharacterFactory CharacterFactory => characterFactory;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        characterFactory = transform.GetChild(0).GetComponent<CharacterFactory>();
    }
}
