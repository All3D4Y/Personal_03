using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : GroupUIBase
{
    Button mainBtn;
    Button exitBtn;
    Button restartBtn;

    protected override void Awake()
    {
        base.Awake();
        mainBtn = transform.GetChild(1).GetChild(1).GetComponent<Button>();
        exitBtn = transform.GetChild(1).GetChild(2).GetComponent<Button>();
        restartBtn = transform.GetChild(1).GetChild(3).GetComponent<Button>();
    }
    void Start()
    {
        OnTransparent();
        mainBtn.onClick.AddListener(GoToMain);
        exitBtn.onClick.AddListener(GameManager.Instance.ExitGame);
        restartBtn.onClick.AddListener(Restart);
    }

    void GoToMain()
    {
        OnTransparent();
        Factory.Instance.CharacterFactory.DestroyAllCharacter();
        GameManager.Instance.BattleUIManager.TransparentAllUI();
        GameManager.Instance.BattleManager.OnTurnEffect.OnTransparent();
        GameManager.Instance.CurrentPosition = null;
        LoadSceneManager.Instance.LoadScene(0);
    }
    
    void Restart()
    {
        OnTransparent();
        Factory.Instance.CharacterFactory.DestroyAllCharacter();
        GameManager.Instance.BattleUIManager.TransparentAllUI();
        GameManager.Instance.BattleManager.OnTurnEffect.OnTransparent();
        LoadSceneManager.Instance.LoadScene(2);
    }
}
