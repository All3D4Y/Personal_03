using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterCodeAndPrefab
    {
        public int code;
        public GameObject prefab;
    }

    public List<CharacterCodeAndPrefab> prefabMappings;
    private Dictionary<int, GameObject> prefabMap;

    private void Awake()
    {
        // Dictionary 초기화
        prefabMap = new Dictionary<int, GameObject>();
        foreach (var mapping in prefabMappings)
        {
            if (!prefabMap.ContainsKey(mapping.code))
            {
                prefabMap[mapping.code] = mapping.prefab;
            }
            else
            {
                Debug.LogWarning($"중복된 코드 {mapping.code}가 감지되었습니다.");
            }
        }
    }


    public Character GenerateCharacter(int code, Transform parent = null)
    {
        // 코드로 프리팹 찾기
        if (!prefabMap.TryGetValue(code, out GameObject prefab))
        {
            Debug.LogWarning($"CharacterFactory: 코드 {code}에 대한 프리팹이 없습니다.");
            return null;
        }

        // 프리팹 인스턴스화
        GameObject characterObject = Instantiate(prefab, parent);
        Character character = characterObject.GetComponent<Character>();

        // 캐릭터 스테이터스 초기화
        character.Initialize();

        return character;
    }
}
