﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusUI : MonoBehaviour
{
    public float minSmoothness = 0.01f;

    Slider hp_Slider;
    Slider mp_Slider;

    RectTransform rect;

    Character character;

    Camera mainCam;

    CanvasGroup buffGroup;

    BuffIconUI[] buffIconUIs;

    void Awake()
    {
        hp_Slider = transform.GetChild(0).GetComponent<Slider>();
        mp_Slider = transform.GetChild(1).GetComponent<Slider>();
        rect = GetComponent<RectTransform>();
        mainCam = Camera.main;

        buffGroup = transform.GetChild(2).GetComponent<CanvasGroup>();
        buffIconUIs = new BuffIconUI[3];
        for (int i = 0; i < buffIconUIs.Length; i++)
        {
            buffIconUIs[i] = transform.GetChild(2).GetChild(i).GetComponent<BuffIconUI>();
        }
    }

    void OnDisable()
    {
        character.onHPChanged -= HPUpdate;
        character.onMPChanged -= MPUpdate;
    }

    public void Initialize(Character character)
    {
        this.character = character;
        character.CUI = this;
        character.onHPChanged += HPUpdate;
        character.onMPChanged += MPUpdate;
        character.onDie += TransformUpdate;
        hp_Slider.value = character.HP / character.MaxHp;
        mp_Slider.value = character.MP / character.MaxMp;
        TransformUpdate();

        foreach (BuffIconUI buffIconUI in buffIconUIs)
        {
            buffIconUI.Initialize();
        }
    }

    public void TransformUpdate()
    {
        Vector3 characterScreenPos = mainCam.WorldToScreenPoint(character.transform.position);
        rect.position = characterScreenPos;
    }

    void HPUpdate(float hp)
    {
        if (hp <= 0)
        {

        }
        else
            StartCoroutine(SliderSmoothing(hp_Slider, hp));

    }

    void MPUpdate(float mp)
    {
        StartCoroutine(SliderSmoothing(mp_Slider, mp));
    }

    public void SetBuffIcon(BuffType type)
    {
        foreach (BuffIconUI buffIconUI in buffIconUIs)
        {
            if (!buffIconUI.IsActivated())
            {
                buffIconUI.SetIcon(type);
                break;
            }
        }
    }

    public void ClearBuffIcon()
    {
        foreach (BuffIconUI buffIconUI in buffIconUIs)
        {
            buffIconUI.ClearIcon();
        }
    }

    IEnumerator SliderSmoothing(Slider slider, float targetValue)
    {
        // hp가 줄어드는 상황일 때
        if (slider.value > targetValue)   
        {
            while (slider.value > targetValue)
            {
                slider.value -= Mathf.Max(minSmoothness, Time.deltaTime * (targetValue - slider.value));
                if (slider.value <= targetValue)
                    slider.value = targetValue;
                yield return null;
            }
        }
        // hp가 늘어나는 상황일 때
        else if (slider.value < targetValue)
        {
            while (slider.value < targetValue)
            {
                slider.value += Mathf.Max(minSmoothness, Time.deltaTime * (targetValue - slider.value));
                if (slider.value >= targetValue)
                    slider.value = targetValue;
                yield return null;
            }
        }
    }
}
