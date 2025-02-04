using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    CharacterFactory characterFactory;
    CharacterUIPool characterUIPool;

    public CharacterFactory CharacterFactory => characterFactory;
    public CharacterUIPool CharacterUIPool => characterUIPool;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        characterFactory = transform.GetChild(0).GetComponent<CharacterFactory>();
        characterUIPool = transform.GetChild(1).GetComponent<CharacterUIPool>();
    }

    protected override void OnInitialize()
    {
    }
}
