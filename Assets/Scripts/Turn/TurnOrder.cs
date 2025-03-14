using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnOrder
{
    List<Character> characters;         // 모든 캐릭터(아군/적)
    float actionThreshold = 100f;       // 행동 임계값

    public Action onTurnCount;          // 차례가 넘어감을 알리는 델리게이트

    public bool AllEnemiesDefeated => characters.All(c => c.IsPlayer);      // 리스트에 남은 캐릭터가 전부 플레이어다 => 승리
    public bool AllPlayersDefeated => characters.All(c => !c.IsPlayer);     // 리스트에 남은 캐릭터가 전부 적이다 => 패배

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="players">플레이어 캐릭터들의 리스트</param>
    /// <param name="enemies">적 캐릭터들의 리스트</param>
    public void Initialize(List<Character> players, List<Character> enemies)
    {
        // 캐릭터 리스트 초기화
        List<Character> list = new List<Character>();
        for (int i = 0; i < players.Count; i++)
        {
            if (i < 4)                              // i < 4 (대기석은 턴 계산을 하지 않음)
                list.Add(players[i]);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            if (i < 4)
                list.Add(enemies[i]);
        }

        characters = list;
    }

    /// <summary>
    /// 캐릭터 교체 시 턴 계산을 위한 리스트를 갱신
    /// </summary>
    public void ListUpdate()
    {
        BattleManager manager = GameManager.Instance.BattleManager;
        List<Character> list = new List<Character>();

        for (int i = 0; i < 4; i++)
        {
            if (!manager.PlayerSlot.GetSlot(i).IsEmpty)
                list.Add(manager.PlayerSlot.GetSlot(i).CharacterData);
            if (!manager.EnemySlot.GetSlot(i).IsEmpty)
                list.Add(manager.EnemySlot.GetSlot(i).CharacterData);
        }

        characters = list;
    }

    /// <summary>
    /// 다음 차례인 캐릭터를 반환하는 함수
    /// </summary>
    /// <returns>다음 차례인 캐릭터</returns>
    public Character GetNextCharacter()
    {
        // 속도 증가
        IncreaseSpeed();

        // 행동할 캐릭터 찾기
        Character nextCharacter = characters
            .OrderByDescending(c => c.CurrentSpeed)     // 속도가 높은 순으로 정렬
            .FirstOrDefault();                          // 맨 앞 반환 (현재 속도가 가장 높은 캐릭터)

        if (nextCharacter != null)
        {
            // 선택된 캐릭터의 속도 -100, 0 미만으로는 내려가지 않음
            nextCharacter.CurrentSpeed = Mathf.Max(0, nextCharacter.CurrentSpeed - actionThreshold);
        }
        
        Debug.Log($"{nextCharacter.name}의 차례입니다. 속도 : {nextCharacter.CurrentSpeed + 100}");

        return nextCharacter; // 행동할 캐릭터 반환
    }

    /// <summary>
    /// 전투가 끝났는지 확인하기 위한 함수
    /// </summary>
    /// <returns>true가 리턴되면 전투 종료</returns>
    public bool IsBattleOver()
    {
        return AllEnemiesDefeated || AllPlayersDefeated;                // true가 리턴되면 전투 종료
    }

    /// <summary>
    /// 캐릭터들의 속도를 증가시키는 함수
    /// </summary>
    public void IncreaseSpeed()
    {
        // 생존 중인 캐릭터들 속도 증가
        foreach (var character in characters)
        {
            if (character.IsAlive) // 생존 중인 캐릭터만 속도 증가
                character.CurrentSpeed += character.CurrentSpeedIncrement;
        }
    }

    /// <summary>
    /// 턴 계산을 위한 리스트에 캐릭터를 추가하는 함수
    /// </summary>
    /// <param name="character">추가할 캐릭터</param>
    public void AddToList(Character character)
    {
        characters.Add(character);
    }

    /// <summary>
    /// 턴 계산을 위한 리스트에서 캐릭터를 삭제하는 함수
    /// </summary>
    /// <param name="character">삭제할 캐릭터</param>
    public void RemoveFromList(Character character)
    {
        characters.Remove(character);
    }
}