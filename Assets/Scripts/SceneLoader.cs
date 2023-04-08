using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingCanvasPrefab;
    [SerializeField]
    private float loadTime = 1;

    private GameObject loadingCanvas;
    private CanvasGroup canvasGroup;

    private AsyncOperation asyncOperation;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        loadingCanvas = Instantiate(loadingCanvasPrefab);
        DontDestroyOnLoad(loadingCanvas);
        canvasGroup = loadingCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        loadingCanvas.SetActive(true);
        yield return StartCoroutine(fadeLoadingScreen(1, loadTime));
        asyncOperation = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        yield return StartCoroutine(fadeLoadingScreen(0, loadTime));
        loadingCanvas.SetActive(false);

        Destroy(loadingCanvas);
    }

    IEnumerator fadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
}