using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupUIBase : MonoBehaviour
{
    public float visibleSmoothness = 0.1f;

    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnVisible()
    {
        StopCoroutine(TransparentCoroutine());
        StartCoroutine(VisibleCoroutine());
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void OnTransparent()
    {
        StopCoroutine(VisibleCoroutine());
        StartCoroutine(TransparentCoroutine());
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    protected IEnumerator VisibleCoroutine()
    {
        while (canvasGroup.alpha >= 1.0f)
        {
            canvasGroup.alpha += visibleSmoothness * Time.deltaTime;
            yield return null;
        }
    }

    protected IEnumerator TransparentCoroutine()
    {
        while (canvasGroup.alpha <= 0.0f)
        {
            canvasGroup.alpha -= visibleSmoothness * Time.deltaTime;
            yield return null;
        }
    }
}
