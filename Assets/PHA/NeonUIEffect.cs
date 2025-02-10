using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI; // UI ��� ����� ���� �ʿ�

public class NeonUIEffect : MonoBehaviour
{
    public UnityEngine.UI.Image neonImage; // Image Ÿ�� ��Ȯ�ϰ� ����
    public Color[] neonColors = { Color.cyan, Color.magenta, Color.yellow, Color.green }; // �׿� ���� ���
    public Color normalColor = new Color(1f, 1f, 1f, 0.3f); // �⺻ ȭ�� ���� (���� 30%)
    public AudioSource glitchSound; // ġ���� ȿ���� (���� ����)

    private bool isFlickering = false;

    void Start()
    {
        if (neonImage == null)
        {
            UnityEngine.Debug.LogError("�׿� �̹����� �������� �ʾҽ��ϴ�!");
            return;
        }

        neonImage.color = normalColor; // ó������ �⺻ ȭ�� ���� ����
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            float normalTime = UnityEngine.Random.Range(2.0f, 3.5f); // ���� ȭ�� ���� �ð� (�� ���)
            float flickerTime = UnityEngine.Random.Range(0.05f, 0.2f); // �׿� ���� ���� �ð� (�� ª��)
            bool glitch = UnityEngine.Random.value > 0.85f; // 15% Ȯ���� ġ���� ȿ�� �߻�

            // �⺻ ȭ�� ���� (���� 30%)
            StartCoroutine(ChangeColor(normalColor, normalTime));
            yield return new WaitForSeconds(normalTime);

            if (glitch)
            {
                StartCoroutine(GlitchEffect());
                yield return new WaitForSeconds(0.1f);
            }

            // �׿� ���� ���� ���� (���� ����)
            Color newNeonColor = neonColors[UnityEngine.Random.Range(0, neonColors.Length)];
            newNeonColor.a = 0.3f; // ���� 30% ����
            StartCoroutine(ChangeColor(newNeonColor, flickerTime));
            yield return new WaitForSeconds(flickerTime);
        }
    }

    IEnumerator GlitchEffect()
    {
        if (glitchSound != null)
            glitchSound.Play(); // ġ���� �Ҹ� ���

        neonImage.color = new Color(1f, 1f, 1f, 0.3f); // ���������� ���
        yield return new WaitForSeconds(0.05f);
        neonImage.color = normalColor; // ���� �������� ����
    }

    IEnumerator ChangeColor(Color targetColor, float duration)
    {
        if (isFlickering) yield break;

        isFlickering = true;
        float elapsed = 0f;
        Color startColor = neonImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            neonImage.color = Color.Lerp(startColor, targetColor, elapsed / duration);
            yield return null;
        }

        isFlickering = false;
    }
}
