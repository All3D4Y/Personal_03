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

    void StartGame()
    {
        OnTransparent();
        LoadSceneManager.Instance.LoadScene(1);
    }
}
