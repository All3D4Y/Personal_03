using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuffManager
{
    Character character;

    List<IBuff> buffList;

    /// <summary>
    /// 생성자, 캐릭터 클래스마다 하나씩
    /// </summary>
    /// <param name="character">버프를 적용할 캐릭터</param>
    public BuffManager(Character character)
    {
        this.character = character;
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize()
    {
        buffList = new List<IBuff>();
        GameManager.Instance.BattleManager.TurnOrder.onTurnCount += TurnCount;
    }

    /// <summary>
    /// 버프 목록에 버프를 추가
    /// </summary>
    /// <param name="buff">추가할 버프</param>
    public void AddBuff(IBuff buff)
    {
        buffList.Add(buff);
        buff.ElapsedTurn = 0;
    }

    /// <summary>
    /// 버프 적용 및 버프 아이콘 생성
    /// </summary>
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

    /// <summary>
    /// 턴이 지나면 카운트하고 지속기간이 끝난 버프를 목록에서 삭제
    /// </summary>
    public void TurnCount()
    {
        foreach (IBuff buff in buffList)
        {
            buff.ElapsedTurn++;
        }

        buffList.RemoveAll(buff => buff.ElapsedTurn > buff.Duration);
    }
}
