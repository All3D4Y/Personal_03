using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchUI : MonoBehaviour
{
    Character character = null;

    Button button;
    TextMeshProUGUI playerName;
    Slider hpSlider;
    Slider mpSlider;

    public bool IsEmpty => character == null;

    void Awake()
    {
        button = GetComponent<Button>();

        playerName = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        hpSlider = transform.GetChild(3).GetComponent<Slider>();
        mpSlider = transform.GetChild(5).GetComponent<Slider>();
    }

    public void Initialize()
    {
        if (!IsEmpty)
        {
            playerName.text = character.characterName;
            hpSlider.value = character.HP / character.MaxHp;
            mpSlider.value = character.MP / character.MaxMp;
        }
    }

    public void AssignCharacter(Character character)
    {
        this.character = character;
    }
    
    public void Clear()
    {
        character = null;
    }
}
