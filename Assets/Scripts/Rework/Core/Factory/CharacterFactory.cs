using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    public GameObject[] characters;

    Transform[] playerSlotTransforms;
    Transform[] playerStandbySlotTransforms;
    Transform[] enemySlotTransforms;
    Transform[] enemyStandbySlotTransforms;

    void Awake()
    {
        playerSlotTransforms = new Transform[4];
        playerStandbySlotTransforms = new Transform[4];
        enemySlotTransforms = new Transform[4];
        enemyStandbySlotTransforms = new Transform[4];

        Transform child = transform.GetChild(0);
        for (int i = 0; i < playerSlotTransforms.Length; i++)
        {
            playerSlotTransforms[i] = child.GetChild(i);
        }

        child = transform.GetChild(1);
        for (int i = 0; i < playerStandbySlotTransforms.Length; i++)
        {
            playerStandbySlotTransforms[i] = child.GetChild(i);
        }

        child = transform.GetChild(2);
        for (int i = 0; i < enemySlotTransforms.Length; i++)
        {
            enemySlotTransforms[i] = child.GetChild(i);
        }

        child = transform.GetChild(3);
        for (int i = 0; i < enemyStandbySlotTransforms.Length; i++)
        {
            enemyStandbySlotTransforms[i] = child.GetChild(i);
        }
    }

    public Character GenerateCharacter(int prefabIndex, int slotIndex, Transform parent = null)
    {
        // 프리팹 인스턴스화
        GameObject characterObject = Instantiate(characters[prefabIndex], parent);
        Character character = characterObject.GetComponent<Character>();

        // 캐릭터 속성 초기화
        character.Initialize();

        return character;
    }
}
