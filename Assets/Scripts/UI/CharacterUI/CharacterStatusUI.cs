using System.Collections;
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

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="character">이 UI가 표시할 정보를 가진 캐릭터</param>
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

    /// <summary>
    /// UI의 위치 갱신
    /// </summary>
    public void TransformUpdate()
    {
        Vector3 characterScreenPos = mainCam.WorldToScreenPoint(character.transform.position);
        rect.position = characterScreenPos;
    }

    /// <summary>
    /// 체력 갱신
    /// </summary>
    /// <param name="hp">현재 체력의 비율</param>
    void HPUpdate(float hp)
    {
        if (hp <= 0)
        {
            return;
        }
        else
            StartCoroutine(SliderSmoothing(hp_Slider, hp));

    }

    /// <summary>
    /// 마인드 갱신
    /// </summary>
    /// <param name="mp">현재 마인드의 비율</param>
    void MPUpdate(float mp)
    {
        StartCoroutine(SliderSmoothing(mp_Slider, mp));
    }

    /// <summary>
    /// 버프 적용 시 아이콘을 표시하는 함수
    /// </summary>
    /// <param name="type">버프의 종류</param>
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

    /// <summary>
    /// 버프 아이콘을 끄는 함수
    /// </summary>
    public void ClearBuffIcon()
    {
        foreach (BuffIconUI buffIconUI in buffIconUIs)
        {
            buffIconUI.ClearIcon();
        }
    }

    /// <summary>
    /// 슬라이더를 부드럽게 움직이는 코루틴
    /// </summary>
    /// <param name="slider">움직일 슬라이더</param>
    /// <param name="targetValue">목표 값</param>
    /// <returns></returns>
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
