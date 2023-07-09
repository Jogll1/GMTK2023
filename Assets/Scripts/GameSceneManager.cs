using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public bool fadeIn;

    private void Start()
    {
        if (fadeIn) StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    public void ReloadScene()
    {
        StartCoroutine(_ReloadScene());
    }

    IEnumerator _LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator _ReloadScene()
    {
        yield return StartCoroutine(_LoadScene(SceneManager.GetActiveScene().name));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.color = Color.black;
        fadeImage.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(string sceneName)
    {
        fadeImage.color = Color.clear;
        fadeImage.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // fadeImage.gameObject.SetActive(false);

        StartCoroutine(_LoadScene(sceneName));
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
# endif
    }
}
