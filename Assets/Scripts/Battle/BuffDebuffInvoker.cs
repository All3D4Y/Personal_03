using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffInvoker
{
    static Queue<IBuffDebuff> undoQueue = new Queue<IBuffDebuff>();

    public static void DoBuffDebuff(IBuffDebuff buff)
    {
        buff.Do();
        undoQueue.Enqueue(buff);
    }

    public static void UndoBuffDebuff()
    {
        if (undoQueue.Count > 0)
        {
            IBuffDebuff temp = undoQueue.Dequeue();
            temp.Undo();
        }
    }
}
