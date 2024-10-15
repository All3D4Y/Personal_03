using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public string[] characters;
    public GameObject characterPrefab;

    List<EntityBase> entities;

    void Awake()
    {
        entities = new List<EntityBase>();

        for (int i = 0; i < characters.Length; i++)
        {
            GameObject temp = Instantiate(characterPrefab);
            TestCharacter character = temp.GetComponent<TestCharacter>();
            character.Setup(characters[i]);

            entities.Add(character);
        }
    }

    void Update()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Updated();
        }
    }
}
