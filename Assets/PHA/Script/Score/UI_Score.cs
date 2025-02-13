using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_Score : MonoBehaviour
{
    public Slider scoreSlider;
    public TextMeshProUGUI scoreText;
    public Transform textPosition;         // ���� ȹ�� ����Ʈ ��ġ
    public TextMeshProUGUI floatingTextPrefab;
    public int maxScore = 100;
    private int currentScore = 0;
    private TextMeshProUGUI activeFloatingText;

    public SpecialQuestManager SpecialQuestManager;

    public CanvasGroup canvasGroup; // CanvasGroup ����
    public float fadeDuration = 3.0f; // ���̵� ���� �ð�
    private bool isQuestChecked = false; // �� ���� ����ǵ��� �÷��� �߰�

    public setDayData dataManger;
    public GameObject Clear;

    void Start()
    {
        canvasGroup.alpha = 0; // ó���� ����

        maxScore = dataManger.setScoreMax();
        UnityEngine.Debug.Log(maxScore);

        if (scoreSlider != null)
        {
            scoreSlider.maxValue = maxScore;
            scoreSlider.value = currentScore;
        }

        if (SpecialQuestManager == null)
        {
            SpecialQuestManager = FindObjectOfType<SpecialQuestManager>();
        }

        if (SpecialQuestManager == null)
        {
            Debug.LogError("UI_Score: SpecialQuestManager�� ã�� �� �����ϴ�!");
        }

        UpdateScoreUI();
    }

    private void Update()
    {
        if (currentScore == maxScore && !isQuestChecked)
        {
            isQuestChecked = true; // ���� �� �ٽ� ������� �ʵ��� ����
            SpecialQuestManager.CheckPublicAuthorityQuestComplete();
            SpecialQuestManager.CheckRevolutionaryArmyQuestComplete();
            SpecialQuestManager.CheckCultQuestComplete();
            SpecialQuestManager.CheckCrimeSyndicateQuestComplete();

            Clear.SetActive(true);

            Invoke("StartFadeOut", 1f);
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        float startAlpha = canvasGroup.alpha; // 0�̾�� ��

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, elapsedTime / fadeDuration); // 0 �� 1
            yield return null;
        }

        canvasGroup.alpha = 1; // ������ ����������
        Debug.Log("���̵� �� �Ϸ�!");

        SceneManager.LoadScene("StoryScene");
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        currentScore = Mathf.Clamp(currentScore, 0, maxScore);

        if (scoreSlider != null)
        {
            scoreSlider.value = currentScore;
        }

        UpdateScoreUI();
        ShowFloatingText(amount);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{currentScore} / {maxScore}";

        }
    }

    private void ShowFloatingText(int amount)
    {
        // ���� �ؽ�Ʈ�� ������ ����
        if (activeFloatingText != null)
        {
            StopAllCoroutines(); // ���� �ִϸ��̼� �ߴ�
            Destroy(activeFloatingText.gameObject);
        }

        // �� �ؽ�Ʈ ����
        activeFloatingText = Instantiate(floatingTextPrefab, textPosition.position, Quaternion.identity, textPosition);
        activeFloatingText.text = $"+{amount}";

        StartCoroutine(FadeOutAndMove(activeFloatingText));
    }

    private IEnumerator FadeOutAndMove(TextMeshProUGUI floatingText)
    {
        float duration = 1f; // �ִϸ��̼� ���� �ð�
        float elapsed = 0f;
        Color color = floatingText.color;
        Vector3 startPos = floatingText.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 20, 0); // ���� 50px �̵�

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            floatingText.color = new Color(color.r, color.g, color.b, alpha);
            floatingText.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        Destroy(floatingText.gameObject); // �ִϸ��̼� ���� �� ����
        activeFloatingText = null; // ���� Ȱ��ȭ�� �ؽ�Ʈ �ʱ�ȭ
    }
}
