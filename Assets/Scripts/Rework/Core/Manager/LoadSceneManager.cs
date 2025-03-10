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
        waitTime = new WaitForSeconds(1.0f);
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        fadeImage = transform.GetChild(0).GetComponent<CanvasGroup>();
        loadText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(FadeIn(sceneIndex));
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn(int sceneIndex)
    {
        if (sceneIndex == 1)
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
