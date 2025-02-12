using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��� ����� ���� �ʿ�

public class NeonUIEffect : MonoBehaviour
{
    public Image neonImage;
    public Color[] neonColors = { Color.cyan, Color.magenta, Color.yellow, Color.green };
    public Color normalColor = new Color(1f, 1f, 1f, 0.3f);
    public AudioSource glitchSound;

    private bool isFlickering = false;
    private Color currentColor;
    private int colorIndex = 0;

    void Start()
    {
        if (neonImage == null)
        {
            Debug.LogError("�׿� �̹����� �������� �ʾҽ��ϴ�!");
            return;
        }

        currentColor = normalColor;
        neonImage.color = currentColor;
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            float normalTime = Random.Range(2.0f, 3.5f);
            float flickerTime = Random.Range(0.5f, 1.5f); // �ڿ������� ���� ��ȭ �ð�

            bool glitch = Random.value > 0.85f;

            // ���� ������ ����
            Color previousColor = neonColors[colorIndex];

            // ���� �׿� �������� ���� (��ȯ)
            colorIndex = (colorIndex + 1) % neonColors.Length;
            Color newNeonColor = neonColors[colorIndex];
            newNeonColor.a = 0.3f;

            // ���� ����� �ڿ������� ����ǵ���
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
        StartCoroutine(ChangeColor(glitchColor, 0.1f));
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(ChangeColor(currentColor, 0.2f));
    }

    IEnumerator ChangeColor(Color targetColor, float duration)
    {
        if (isFlickering) yield break;

        isFlickering = true;
        float elapsed = 0f;
        Color startColor = neonImage.color;
        currentColor = targetColor;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            neonImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        isFlickering = false;
    }
}
