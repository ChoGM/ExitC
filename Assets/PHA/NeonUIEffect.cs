using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NeonUIEffect : MonoBehaviour
{
    public Image neonImage; // �׿� ȿ���� ������ UI �̹���
    public Color baseColor = Color.cyan; // �⺻ �׿� ����
    public Color normalColor = Color.white; // ���� ȭ�� ����
    public AudioSource glitchSound; // ġ���� ȿ���� (���� ����)

    private bool isFlickering = false;

    void Start()
    {
        if (neonImage == null)
        {
            Debug.LogError("�׿� �̹����� �������� �ʾҽ��ϴ�!");
            return;
        }

        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            float normalTime = Random.Range(2.0f, 3.5f); // ���� ȭ���� �����Ǵ� �ð� (�� ���)
            float flickerTime = Random.Range(0.05f, 0.2f); // �׿� ������ �ð� (�� ª��)
            bool glitch = Random.value > 0.85f; // 15% Ȯ���� ġ���� ȿ�� �߻�

            // ���� ȭ�� ���� (�� ����)
            StartCoroutine(ChangeColor(normalColor, normalTime));
            yield return new WaitForSeconds(normalTime);

            if (glitch)
            {
                StartCoroutine(GlitchEffect());
                yield return new WaitForSeconds(0.1f);
            }

            // ª�� �׿� ȿ�� ��¦��
            StartCoroutine(ChangeColor(baseColor, flickerTime));
            yield return new WaitForSeconds(flickerTime);
        }
    }

    IEnumerator GlitchEffect()
    {
        if (glitchSound != null)
            glitchSound.Play(); // ġ���� �Ҹ� ���

        neonImage.color = baseColor * 0.8f; // ��¦ ��ο�������
        yield return new WaitForSeconds(0.05f);
        neonImage.color = normalColor; // ���� ������ ����
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
