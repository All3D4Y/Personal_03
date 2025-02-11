using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuffManager
{
    Character character;

    List<IBuff> buffList;

    public BuffManager(Character character)
    {
        character = this.character;
        buffList = new List<IBuff>();
    }

    public void AddBuff(IBuff buff)
    {
        buffList.Add(buff);
    }
    public void RemoveBuff(IBuff buff)
    {
        buffList.Remove(buff);
    }

    public void BuffUpdate()
    {
        character.BuffOff();
        character.CUI.ClearBuffIcon();

        foreach (IBuff buff in buffList)
        {
            Skill skill = buff as Skill;
            skill.Affect(character, character);
            character.CUI.SetBuffIcon(buff.Type);  // 버프 아이콘 활성화
        }
    }
}
