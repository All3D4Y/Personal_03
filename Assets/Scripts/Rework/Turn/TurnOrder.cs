using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TurnOrder
{
    private List<Character> characters; // 모든 캐릭터(아군/적)
    private float actionThreshold = 100f; // 행동 임계값

    public void Initialize(List<Character> players, List<Character> enemies)
    {
        // 캐릭터 리스트 초기화
        characters = new List<Character>(players.Concat(enemies));
    }

    public Character GetNextCharacter()
    {
        // 속도 증가
        foreach (var character in characters)
        {
            if (!character.IsAlive) // 생존 중인 캐릭터만 속도 증가
                character.CurrentSpeed += character.CurrentSpeedIncrement;
        }

        // 행동할 캐릭터 찾기
        Character nextCharacter = characters
            .Where(c => !c.IsAlive && c.CurrentSpeed >= actionThreshold) // 행동 조건
            .OrderByDescending(c => c.CurrentSpeed) // 속도가 높은 순으로 정렬
            .FirstOrDefault();

        if (nextCharacter != null)
        {
            // 선택된 캐릭터의 속도를 초기화
            nextCharacter.CurrentSpeed = 0;
        }

        return nextCharacter; // 행동할 캐릭터 반환
    }

    public void RemoveCharacter(Character character)
    {
        // 캐릭터 제거
        characters.Remove(character);
    }

    public bool IsBattleOver()
    {
        // 적 또는 아군이 모두 쓰러졌는지 확인
        bool allPlayersDefeated = characters.All(c => c.IsPlayer && c.IsAlive);
        bool allEnemiesDefeated = characters.All(c => !c.IsPlayer && c.IsAlive);

        return allPlayersDefeated || allEnemiesDefeated;
    }

    public List<Character> GetTurnOrder()
    {
        // 디버깅 및 UI 업데이트용으로 캐릭터들의 현재 속도 반환
        return characters.OrderByDescending(c => c.CurrentSpeed).ToList();
    }
}