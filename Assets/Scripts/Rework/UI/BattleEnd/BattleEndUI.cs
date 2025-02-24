using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleEndUI : MonoBehaviour
{
    TextMeshProUGUI winOrLose;
    SpoilsUI[] spoils;
    Slider expSlider;
    TextMeshProUGUI currentExp;
    TextMeshProUGUI maxExp;
    Button continueButton;

    StageData stageData;

    void Awake()
    {
        spoils = new SpoilsUI[6];
        Transform child;

        child = transform.GetChild(1).GetChild(0);
        winOrLose = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1).GetChild(1);
        for (int i = 0; i < 6; i++)
        {
            if (i < 3)
                spoils[i] = child.GetChild(0).GetChild(i).GetComponent<SpoilsUI>();
            else
                spoils[i] = child.GetChild(1).GetChild(i).GetComponent<SpoilsUI>();
        }

        child = transform.GetChild(1).GetChild(2);
        expSlider = child.GetChild(1).GetComponent<Slider>();
        
        child = child.GetChild(2);
        currentExp = child.GetChild(0).GetComponent<TextMeshProUGUI>();
        maxExp = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1).GetChild(3);
        continueButton = child.GetComponent<Button>();
    }

    public void Initialize()
    {

    }
}
