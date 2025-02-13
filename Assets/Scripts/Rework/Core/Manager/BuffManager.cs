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
        this.character = character;
    }

    public void Initialize()
    {
        buffList = new List<IBuff>();
        GameManager.Instance.BattleManager.TurnOrder.onTurnCount += TurnCount;
    }

    public void AddBuff(IBuff buff)
    {
        buffList.Add(buff);
        buff.ElapsedTurn = 0;
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

    public void TurnCount()
    {
        foreach (IBuff buff in buffList)
        {
            buff.ElapsedTurn++;
        }

        buffList.RemoveAll(buff => buff.ElapsedTurn > buff.Duration);
    }
}
