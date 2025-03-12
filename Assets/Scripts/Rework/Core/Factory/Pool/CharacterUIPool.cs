using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterUIPool : MonoBehaviour
{
    public GameObject characterUIPrefab;

    public Transform parentTransform;

    public void Initialize()
    {
        parentTransform = FindAnyObjectByType<CharacterStatusGroupUI>().transform;
        BattleManager battleManager = GameManager.Instance.BattleManager;
        List<Character> temp = battleManager.PlayerParty.Concat(battleManager.EnemyParty).ToList();
        foreach (Character c in temp)
        {
            GenerateUI().Initialize(c);
        }
    }

    CharacterStatusUI GenerateUI()
    {
        CharacterStatusUI result;
        GameObject uiPrefab = Instantiate(characterUIPrefab, parentTransform);
        result = uiPrefab.GetComponent<CharacterStatusUI>();
        return result;
    }
}
