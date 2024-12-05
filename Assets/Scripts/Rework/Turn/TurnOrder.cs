using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TurnOrder
{
    List<Character> characters;         // 모든 캐릭터(아군/적)
    float actionThreshold = 100f;       // 행동 임계값

    public void Initialize(List<Character> players, List<Character> enemies)
    {
        // 캐릭터 리스트 초기화
        characters = new List<Character>(players.Concat(enemies));
        
        // 죽으면 리스트에서 빠지도록 델리게이트 등록
        foreach (Character c in characters)
        {
            c.onDie -= (character) => RemoveCharacter(character);
            c.onDie += (character) => RemoveCharacter(character);
        }
    }

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

    public void RemoveCharacter(Character character)
    {
        if (characters.Contains(character))
        {
            characters.Remove(character);
            Debug.Log($"{character.Name}가 TurnOrder 리스트에서 제거되었습니다.");
        }
    }

    public bool IsBattleOver()
    {
        // 적 또는 아군이 모두 쓰러졌는지 확인
        bool allPlayersDefeated = characters.All(c => c.IsPlayer);      // 리스트에 남은 캐릭터가 전부 플레이어다 => 승리
        bool allEnemiesDefeated = characters.All(c => !c.IsPlayer);     // 리스트에 남은 캐릭터가 전부 적이다 => 패배

        return allPlayersDefeated || allEnemiesDefeated;        // true가 리턴되면 배틀 종료
    }

    public List<Character> GetTurnOrder()
    {
        // 디버깅 및 UI 업데이트용으로 캐릭터들의 현재 속도 반환
        return characters.OrderByDescending(c => c.CurrentSpeed).ToList();
    }

    public void IncreaseSpeed()
    {
        // 생존 중인 캐릭터들 속도 증가
        foreach (var character in characters)
        {
            if (!character.IsAlive) // 생존 중인 캐릭터만 속도 증가
                character.CurrentSpeed += character.CurrentSpeedIncrement;
        }
    }
}