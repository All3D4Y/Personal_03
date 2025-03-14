using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    Button button;

    void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(Back);
    }

    /// <summary>
    /// 메인 씬으로 이동
    /// </summary>
    void Back()
    {
        LoadSceneManager.Instance.LoadScene(0);
    }
}
