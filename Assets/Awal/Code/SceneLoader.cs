using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Image loadingFill;
    public float duration = 3f;

    void Start()
    {
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        float timer = 0f;
        float fillRate = 1f / duration;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / duration);
            loadingFill.fillAmount = progress;
            yield return null;
        }

        loadingFill.fillAmount = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
