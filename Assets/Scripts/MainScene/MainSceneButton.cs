using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneButton : GroupUIBase
{
    Button startBtn;
    Button exitBtn;

    protected override void Awake()
    {
        base.Awake();

        startBtn = transform.GetChild(0).GetComponent<Button>();
        exitBtn = transform.GetChild(1).GetComponent<Button>();

        startBtn.onClick.AddListener(StartGame);
        exitBtn.onClick.AddListener(GameManager.Instance.ExitGame);
    }

    /// <summary>
    /// 메인화면에서 미니맵 씬으로 이동
    /// </summary>
    void StartGame()
    {
        OnTransparent();
        LoadSceneManager.Instance.LoadScene(1);
    }
}
