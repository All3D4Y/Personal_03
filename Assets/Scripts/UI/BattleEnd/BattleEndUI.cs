using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleEndUI : GroupUIBase
{
    public float minSmoothness = 0.01f;

    TextMeshProUGUI winOrLose;
    SpoilsUI[] spoils;
    Slider expSlider;
    TextMeshProUGUI levelUp;
    TextMeshProUGUI currentExp;
    TextMeshProUGUI maxExp;
    Button continueButton;

    StageData stageData;

    protected override void Awake()
    {
        base.Awake();
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
                spoils[i] = child.GetChild(1).GetChild(i - 3).GetComponent<SpoilsUI>();
        }

        child = transform.GetChild(1).GetChild(2);
        expSlider = child.GetChild(1).GetComponent<Slider>();
        levelUp = child.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
        
        child = child.GetChild(2);
        currentExp = child.GetChild(0).GetComponent<TextMeshProUGUI>();
        maxExp = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1).GetChild(3);
        continueButton = child.GetComponent<Button>();

        continueButton.onClick.AddListener(Continue);
    }

    void Start()
    {
        OnTransparent();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="stageData">전투 데이터</param>
    /// <param name="isWin">승리했는지</param>
    public void Initialize(StageData stageData, bool isWin)
    {
        this.stageData = stageData;

        // 승리 시
        if (isWin)
        {
            winOrLose.text = "Win!";

            // 전리품 표시
            for (int i = 0; i < 6; i++)
            {
                if (i < stageData.spoils.Length)
                {
                    spoils[i].AssignItem(stageData.spoils[i].item, stageData.spoils[i].count);
                    spoils[i].Initialize(); 
                }
                else
                {
                    spoils[i].gameObject.SetActive(false);
                }
            }
            // 경험치 슬라이더 초기화
            maxExp.text = PlayerDataManager.Instance.LevelUpThreshold.ToString();
            currentExp.text = PlayerDataManager.Instance.Exp.ToString();
            expSlider.value = PlayerDataManager.Instance.LevelUpRatio;

            PlayerDataManager.Instance.onLevelUp -= LevelUp;
            PlayerDataManager.Instance.onLevelUp += LevelUp;

            GetExp();
        }
        // 패배 시
        else
        {
            winOrLose.text = "Lose!";

            for (int i = 0; i < spoils.Length; i++)
            {
                spoils[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 얻은 경험치를 처리하는 함수
    /// </summary>
    public void GetExp()
    {
        float exp = stageData.exp;
        PlayerDataManager.Instance.Exp += exp;
        StartCoroutine(SliderSmoothing(PlayerDataManager.Instance.LevelUpRatio));
    }

    /// <summary>
    /// 경험치가 레벨업 상한치를 넘었을 때 실행되는 함수
    /// </summary>
    public void LevelUp()
    {
        levelUp.enabled = true;
        StartCoroutine(DisableTimer(1));
    }

    /// <summary>
    /// 전투를 끝내고 미니맵으로 돌아가는 함수
    /// </summary>
    public void Continue()
    {
        // 미니맵 씬으로
        LoadSceneManager.Instance.LoadScene(1);
    }

    /// <summary>
    /// 경험치 슬라이더를 부드럽게 움직이는 함수
    /// </summary>
    /// <param name="targetValue">목표 수치</param>
    /// <returns></returns>
    IEnumerator SliderSmoothing(float targetValue)
    {
        // 1.5초 후에
        yield return new WaitForSeconds(1.5f);

        // Value가 늘어나는 상황일 때
        if (expSlider.value < targetValue)
        {
            while (expSlider.value < targetValue)
            {
                expSlider.value += Mathf.Max(minSmoothness, Time.deltaTime * (targetValue - expSlider.value));
                if (expSlider.value >= targetValue)
                    expSlider.value = targetValue;
                yield return null;
            }
            if (expSlider.value == 1)
                expSlider.value--;
        }
        // LevelUp인 상황
        else if (expSlider.value > targetValue)
        {
            while (expSlider.value <= 1)
            {
                expSlider.value += Mathf.Max(minSmoothness, Time.deltaTime * (targetValue - expSlider.value));
                if (expSlider.value >= targetValue)
                    expSlider.value = targetValue;
                yield return null;
            }
            expSlider.value = 0;
            while(expSlider.value < targetValue)
            {
                expSlider.value += Mathf.Max(minSmoothness, Time.deltaTime * (targetValue - expSlider.value));
                if (expSlider.value >= targetValue)
                    expSlider.value = targetValue;
                yield return null;
            }
        }
    }

    /// <summary>
    /// 레벨업 효과를 일정 시간 후 비활성화 하는 코루틴
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator DisableTimer(float time)
    {
        yield return new WaitForSeconds(time);
        levelUp.enabled = false;
    }
}
