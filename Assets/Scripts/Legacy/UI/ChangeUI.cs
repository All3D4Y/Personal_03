using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour
{
    BattleSlot slot;

    Button button;
    TextMeshProUGUI allyName;
    Slider hpSlider;
    Slider mpSlider;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSwap);

        allyName = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        hpSlider = transform.GetChild(3).GetComponent<Slider>();
        mpSlider = transform.GetChild(5).GetComponent<Slider>();
    }

    public void SetSlot(BattleSlot slot)
    {
        this.slot = slot;
    }

    public void GetData()
    {
        if (slot != null)
        {
            allyName.text = slot.ActorData.ActorName;
            hpSlider.value = slot.ActorData.HP / slot.ActorData.MaxHP;
            mpSlider.value = slot.ActorData.MP / slot.ActorData.MaxMP; 
        }
    }

    public void ClearData()
    {
        if (slot != null)
        {
            allyName.text = "Ally Name";
            hpSlider.value = 0.5f;
            mpSlider.value = 0.5f;
            slot = null; 
        }
    }

    void OnSwap()
    {
        if (slot != null)
        {
            BattleSlot temp = OldGameManager.Instance.BattleManager.OnTurnSlot;
            OldGameManager.Instance.SlotVisualizer.SwapSlot(temp, slot); 
            OldGameManager.Instance.BattleManager.SwapCharacter(slot as StandbySlot);
            //SetSlot(temp);
            GetData();
        }
    }
}
