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
        button.onClick.AddListener(SwitchCharacter);

        playerName = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        hpSlider = transform.GetChild(3).GetComponent<Slider>();
        mpSlider = transform.GetChild(5).GetComponent<Slider>();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize()
    {
        if (!IsEmpty)
        {
            playerName.text = character.characterName;
            hpSlider.value = character.HP / character.MaxHp;
            mpSlider.value = character.MP / character.MaxMp;
        }
    }

    /// <summary>
    /// 교체 UI에 캐릭터를 할당하는 함수
    /// </summary>
    /// <param name="character">할당 할 캐릭터</param>
    public void AssignCharacter(Character character)
    {
        this.character = character;
    }
    
    /// <summary>
    /// 할당된 캐릭터를 삭제하는 함수
    /// </summary>
    public void Clear()
    {
        character = null;
    }

    /// <summary>
    /// 현재 차례인 캐릭터와 대기석의 캐릭터를 교체하는 함수
    /// </summary>
    public void SwitchCharacter()
    {
        Character character = this.character;
        BattleManager manager = GameManager.Instance.BattleManager;
        // 1. 슬롯 업데이트, 위치 업데이트
        manager.PlayerSlot.SwapCharacter(character.Index, manager.OnTurnCharacter.Index);
        Clear();
        AssignCharacter(manager.OnTurnCharacter);
        Initialize();
        // 2. 턴오더 리스트 업데이트
        manager.TurnOrder.RemoveFromList(manager.OnTurnCharacter);
        manager.TurnOrder.AddToList(character);
        manager.SetTurnCharacter(character);
        // 3. UI 초기화
        GameManager.Instance.BattleUIManager.Clear();
        GameManager.Instance.BattleUIManager.Initialize();
    }
}
