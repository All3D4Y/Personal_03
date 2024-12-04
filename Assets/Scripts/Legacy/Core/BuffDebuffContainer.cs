using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffContainer
{
    static List<IOldBuffDebuff> buffDebuffs = new List<IOldBuffDebuff>();

    public static List<IOldBuffDebuff> BuffDebuffs => buffDebuffs;

    public static void SaveBuff(IOldBuffDebuff buffDebuff)
    {
        buffDebuffs.Add(buffDebuff);
    }

    public static void DeleteBuff(IOldBuffDebuff buffDebuff)
    {
        buffDebuffs.Remove(buffDebuff);
    }
}
