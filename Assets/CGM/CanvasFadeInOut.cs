using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFadeInOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup; // ĵ���� �׷� ����
    [SerializeField] private float fadeInTime = 1f; // ���̵� �� �ð�
    [SerializeField] private float holdTime = 1f; // ���� �ð�
    [SerializeField] private float fadeOutTime = 1f; // ���̵� �ƿ� �ð�

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // ���̵� ��
        yield return StartCoroutine(Fade(0, 1, fadeInTime));

        // ����
        yield return new WaitForSeconds(holdTime);

        // ���̵� �ƿ�
        yield return StartCoroutine(Fade(1, 0, fadeOutTime));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha; // ������ �� ����
    }
}