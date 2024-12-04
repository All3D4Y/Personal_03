using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public int[] players;

    void Reset()
    {
        players = new int[8];
    }
}
