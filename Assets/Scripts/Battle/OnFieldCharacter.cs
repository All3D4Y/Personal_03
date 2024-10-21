using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFieldCharacter : MonoBehaviour
{
    public CharacterData[] OnFieldCharacters {  get; private set; }

    public void InsertCharacter()
    {
        
    }

    public void ExtractCharacter()
    {
    }

    public void ResetCharacter()
    {
        OnFieldCharacters = null;
    }
    
    public void TestInsert()
    {
        OnFieldCharacters = new CharacterData[5];

        for (int i = 0; i < OnFieldCharacters.Length; i++)
        {
            OnFieldCharacters[i] = GameManager.Instance.CharacterDataManager[(uint)i] as CharacterData;
        }
    }
}
