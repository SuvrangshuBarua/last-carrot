using UnityEngine;
using TMPro;
using System.Collections;

public class TitleAnimations : MonoBehaviour
{
    [SerializeField] TMP_Text title;

    [SerializeField] float titleHoldTimer = 5f;

    [SerializeField] float colorIncr = 0.01f;
    [SerializeField] float colorIncrWait = 0.001f;

    [SerializeField] float sizeIncr = 0.01f;
    [SerializeField] float sizeIncrWait = 0.001f;

    Color originalColor;
    float originalSize;

    Coroutine showHideCor;

    private void Awake()
    {
        if (title == default)
        {
            Debug.LogError("No title set");
            return;
        }

        originalColor = title.color;
        originalSize = title.fontSize;
    }

    public void OnWave(int waveIndex)
    {
        ShowTitle();
    }

    [ContextMenu("Show Title")]
    public void ShowTitle()
    {
        if (showHideCor != default)
        {
            StopCoroutine(showHideCor);
        }

        showHideCor = StartCoroutine(ShowHideTitleCor());
    }

    IEnumerator ShowHideTitleCor()
    {
        StartCoroutine(FadeInOutCor(true)); 
        StartCoroutine(SizeInOutCor(true));

        yield return new WaitForSeconds(titleHoldTimer + (Mathf.Max(colorIncrWait, sizeIncrWait)));

        StartCoroutine(FadeInOutCor(false));
        StartCoroutine(SizeInOutCor(false));
    }

    IEnumerator FadeInOutCor(bool fadeIn)
    {
        Color col = originalColor;
        col.a = fadeIn ? 0 : 1;

        title.color = col;

        for (float i = 0; i <= 1; i+=colorIncr)
        {
            float alpha = Mathf.Lerp(0, 1, i);

            col.a = fadeIn ? alpha : 1 - alpha;

            title.color = col;

            yield return new WaitForSeconds(colorIncrWait);
        }

        col.a = fadeIn ? 1 : 0;

        title.color = col;
    }

    IEnumerator SizeInOutCor(bool fadeIn)
    {
        float size = originalSize;
        size = fadeIn ? 0 : originalSize;

        title.fontSize = size;

        for (float i = 0; i <= 1; i += sizeIncr)
        {
            float temp = Mathf.Lerp(0, originalSize, i);

            size = fadeIn ? temp : originalSize - temp;

            title.fontSize = size;

            yield return new WaitForSeconds(sizeIncrWait);
        }

        size = fadeIn ? originalSize : 0;

        title.fontSize = size;
    }

    private void OnDestroy()
{
    if (showHideCor != null)
    {
        StopCoroutine(showHideCor);
        showHideCor = null;
    }
}
}