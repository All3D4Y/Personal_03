using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [System.Serializable]
    // 프리팹에 코드를 매핑하기 위한 구조체, 인스펙터에서 사용하기 위해 직렬화
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
#if UNITY_EDITOR
                Debug.LogWarning($"코드 {mapping.code}는 중복된 키값입니다."); 
#endif
            }
        }
    }

    /// <summary>
    /// 캐릭터를 생성하는 함수
    /// </summary>
    /// <param name="code">캐릭터의 코드</param>
    /// <param name="parent">캐릭터가 생성될 부모의 트랜스폼</param>
    /// <returns></returns>
    public Character GenerateCharacter(int code, Transform parent = null)
    {
        // 코드로 프리팹 찾기
        if (!prefabMap.TryGetValue(code, out GameObject prefab))
        {
            Debug.LogWarning($"{this.name}: 코드 {code}에 맞는 프리팹이 없습니다.");
            return null;
        }

        // 프리팹 인스턴스화
        GameObject characterObject = Instantiate(prefab, parent);
        Character character = characterObject.GetComponent<Character>();

        // 캐릭터 스테이터스 초기화
        character.Initialize();

        return character;
    }

    /// <summary>
    /// 캐릭터 일괄 파괴
    /// </summary>
    public void DestroyAllCharacter()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
