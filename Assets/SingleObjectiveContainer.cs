using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(LayoutElement))]
[RequireComponent(typeof(CanvasGroup))]
public class SingleObjectiveContainer : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private float animationDuration = 1.0f;

    [SerializeField]
    private float padding = 4.0f;

    private RectTransform rectTransform;
    private LayoutElement layoutElement;
    private CanvasGroup canvasGroup;
    private Coroutine animationCoroutine = null;

    private void Awake()
    {
        rectTransform = this.transform as RectTransform;
        layoutElement = GetComponent<LayoutElement>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetText(string text)
    {
        this.text.text = text;
        Canvas.ForceUpdateCanvases();

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(DoAnimation(true, animationDuration, null));
    }

    public void Hide(System.Action onComplete)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(DoAnimation(false, animationDuration, onComplete));
    }

    IEnumerator DoAnimation(bool show, float duration, System.Action onComplete)
    {
        float startHeight = 0f;
        float endHeight = 0f;
        float startWidth = 0f;
        float endWidth = 0f;

        if (show)
        {
            startHeight = rectTransform.rect.height;
            endHeight = text.rectTransform.rect.height;
            startWidth = rectTransform.rect.width;
            endWidth = text.rectTransform.rect.width;
        }
        else
        {
            startHeight = rectTransform.rect.height;
            endHeight = 0f;
            startWidth = rectTransform.rect.width;
            endWidth = 0f;
        }

        float time = 0.0f;
        while (time < duration)
        {
            layoutElement.preferredHeight = Util.EaseInOut(startHeight, endHeight, time, duration);
            layoutElement.preferredWidth = Util.EaseInOut(startWidth, endWidth, time, duration);
            yield return null;
            time += Time.deltaTime;
        }

        layoutElement.preferredHeight = endHeight;
        layoutElement.preferredWidth = endWidth;

        if (onComplete != null)
        {
            onComplete();
        }
    }
}