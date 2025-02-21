using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupUIBase : MonoBehaviour
{
    protected CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup => canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnVisible()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void OnTransparent()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.0f;
    }
}
