using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CanvasGroup 컴포넌트를 사용하는 UI들이 상속받을 부모클래스
/// </summary>
public class GroupUIBase : MonoBehaviour
{
    protected CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup => canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 보이게하기
    /// </summary>
    public void OnVisible()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    /// <summary>
    /// 숨기기
    /// </summary>
    public void OnTransparent()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.0f;
    }
}
