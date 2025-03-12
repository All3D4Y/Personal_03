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

    void Back()
    {
        LoadSceneManager.Instance.LoadScene(0);
    }
}
