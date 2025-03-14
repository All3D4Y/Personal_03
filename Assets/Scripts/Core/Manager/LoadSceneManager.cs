using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public float fadeSpeed = 1.0f;

    CanvasGroup fadeImage;

    TextMeshProUGUI loadText;

    WaitForSeconds waitTime;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        waitTime = new WaitForSeconds(0.5f);
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        fadeImage = transform.GetChild(0).GetComponent<CanvasGroup>();
        loadText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// 페이드 효과로 씬 로드
    /// </summary>
    /// <param name="sceneIndex">로드할 씬의 인덱스</param>
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(FadeIn(sceneIndex));
    }
    
    /// <summary>
    /// 씬 로드마다 호출되는 페이드 아웃용 함수
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeOut());
    }

    /// <summary>
    /// 페이드 인 코루틴
    /// </summary>
    /// <param name="sceneIndex">로드할 씬의 인덱스</param>
    /// <returns></returns>
    IEnumerator FadeIn(int sceneIndex)
    {
        if (sceneIndex == 2)
            loadText.enabled = true;
        else
            loadText.enabled = false;

        yield return waitTime;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        async.allowSceneActivation = false;

        while (fadeImage.alpha <= 0.999f)
        {
            fadeImage.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        fadeImage.alpha = 1;
        fadeImage.blocksRaycasts = true;
        yield return waitTime;

        async.allowSceneActivation = true;
    }

    /// <summary>
    /// 페이드 아웃용 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        while (fadeImage.alpha >= 0.001f)
        {
            fadeImage.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        fadeImage.alpha = 0;
        fadeImage.blocksRaycasts = false;
    }
}
