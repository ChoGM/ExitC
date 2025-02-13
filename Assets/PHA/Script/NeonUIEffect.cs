using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��� ����� ���� �ʿ�

public class NeonUIEffect : MonoBehaviour
{
    public Image neonImage1;
    public Image neonImage2; // �߰��� �̹���
    public Color[] neonColors = { Color.cyan, Color.magenta, Color.yellow, Color.green };
    public Color normalColor = new Color(1f, 1f, 1f, 0.3f);
    public AudioSource glitchSound;

    private bool isFlickering = false;
    private Color currentColor;
    private int colorIndex = 0;

    void Start()
    {
        if (neonImage1 == null || neonImage2 == null)
        {
            Debug.LogError("�׿� �̹����� �������� �ʾҽ��ϴ�!");
            return;
        }

        currentColor = normalColor;
        neonImage1.color = currentColor;
        neonImage2.color = currentColor;
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            float normalTime = Random.Range(2.0f, 3.5f);
            float flickerTime = Random.Range(0.5f, 1.5f); // �ڿ������� ���� ��ȭ �ð�

            bool glitch = Random.value > 0.85f;

            // ���� �׿� �������� ���� (��ȯ)
            colorIndex = (colorIndex + 1) % neonColors.Length;
            Color newNeonColor = neonColors[colorIndex];
            newNeonColor.a = 0.3f;

            currentColor = newNeonColor; // ���� ���� ������Ʈ
            yield return StartCoroutine(ChangeColor(newNeonColor, flickerTime));

            if (glitch)
            {
                yield return StartCoroutine(GlitchEffect());
            }
        }
    }

    IEnumerator GlitchEffect()
    {
        if (glitchSound != null)
            glitchSound.Play();

        Color glitchColor = new Color(1f, 1f, 1f, 0.6f);
        yield return StartCoroutine(ChangeColor(glitchColor, 0.1f));
        yield return new WaitForSeconds(0.05f);
        yield return StartCoroutine(ChangeColor(currentColor, 0.2f));
    }

    IEnumerator ChangeColor(Color targetColor, float duration)
    {
        if (isFlickering) yield break;

        isFlickering = true;
        float elapsed = 0f;
        Color startColor1 = neonImage1.color;
        Color startColor2 = neonImage2.color;
        currentColor = targetColor;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            neonImage1.color = Color.Lerp(startColor1, targetColor, t);
            neonImage2.color = Color.Lerp(startColor2, targetColor, t);
            yield return null;
        }

        isFlickering = false;
    }
}