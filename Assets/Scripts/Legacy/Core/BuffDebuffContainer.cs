using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffContainer
{
    static List<IBuffDebuff> buffDebuffs = new List<IBuffDebuff>();

    public static List<IBuffDebuff> BuffDebuffs => buffDebuffs;

    public static void SaveBuff(IBuffDebuff buffDebuff)
    {
        buffDebuffs.Add(buffDebuff);
    }

    public static void DeleteBuff(IBuffDebuff buffDebuff)
    {
        buffDebuffs.Remove(buffDebuff);
    }
}
