using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataManager : MonoBehaviour
{
    public ItemSkillData[] skillDatas;

    public ItemSkillData this[uint index]
    {
        get => skillDatas[index];
        set => skillDatas[index] = value;
    }
}
