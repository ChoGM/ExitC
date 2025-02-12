using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class DialogueManager : MonoBehaviour
{
    public StoryLog storyLog; // ScriptableObject ������
    public UnityEngine.UI.Text dialogueText; // ��ȭ �ؽ�Ʈ UI
    public UnityEngine.UI.Text characterNameText; // ĳ���� �̸� ǥ�ÿ� UI
    public UnityEngine.UI.Image characterImage; // ĳ���� �̹���
    public float typingSpeed = 0.05f; // Ÿ���� �ӵ�
    public int questProgress = 1; // ���� ����Ʈ ���൵ (1����, 2���� ��)

    public CanvasGroup fadeCanvas; // ȭ�� ���̵� ȿ��
    public float fadeDuration = 1.0f; // ���̵� ��/�ƿ� �ӵ�
    public float fadeHoldTime = 1.5f; // ��ο� ���� ���� �ð�

    private string[] currentLogs; // ���� ǥ���� �α� �迭
    private int logIndex = 0; // ���� ��� ���� �� �ε���
    private Coroutine typingCoroutine; // ���� ���� ���� Ÿ���� ȿ��
    private bool isTyping = false; // Ÿ���� ������ ����
    private bool isFading = false; // ���̵� ������ ����

    public LogManager logManager;

    void Start()
    {
        fadeCanvas.alpha = 0; // ���� �� ���� ȭ��
        characterImage.gameObject.SetActive(false); // ó������ �̹��� ����
        dialogueText.text = "";
        characterNameText.text = ""; // ĳ���� �̸� �ʱ�ȭ
        //LoadDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFading)
        {
            if (isTyping)
            {
                // Ÿ���� ���̸� ��ü ���� ��� ���
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentLogs[logIndex - 1];
                isTyping = false;
            }
            else
            {
                // ���� ���� ���
                ShowNextLine();
            }
        }
    }

    public void setImage(UnityEngine.UI.Image image)
    {
        characterImage = image;
    }

    public void setStoryLog(StoryLog log)
    {
        storyLog = log;
    }

    public void setProgress(int progress)
    {
        questProgress = progress;
    }

    public void setName(string name)
    {
        characterNameText.text = name;
    }

    public void LoadDialogue()
    {
        string dayName = "day" + questProgress; // ��: "Day1", "Day2"

        foreach (var entry in storyLog.LogEntries)
        {
            if (entry.dayName == dayName)
            {
                currentLogs = entry.logs;
                logIndex = 0;
                characterImage.gameObject.SetActive(true); // ��ȭ ���� �� �̹��� Ȱ��ȭ
                //characterNameText.text = "ĳ���� �̸�"; // ĳ���� �̸� ���� (�ʿ� �� ���� ����)
                ShowNextLine();
                return;
            }
        }

        // �ش� ����Ʈ ���൵�� �´� �αװ� ���� ���
        dialogueText.text = "��ȭ�� �����ϴ�.";
    }

    void ShowNextLine()
    {
        if (!isFading && (currentLogs == null || logIndex >= currentLogs.Length))
        {
            StartCoroutine(FadeToBlack());
            return;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // ���� Ÿ���� ����
        }

        typingCoroutine = StartCoroutine(TypeSentence(currentLogs[logIndex])); // �� ���� ���
        logIndex++; // ���� �ٷ� �̵�
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // �� ���ھ� �߰�
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    IEnumerator FadeToBlack()
    {
        isFading = true;

        // 1. ȭ���� ��Ӱ� (���̵� �ƿ�)
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }

        // ��ο� ���¿��� �̹��� ���� & �ؽ�Ʈ �ʱ�ȭ
        characterImage.gameObject.SetActive(false);
        dialogueText.text = "";
        characterNameText.text = "";

        yield return new WaitForSeconds(fadeHoldTime); // ���� �ð� ����

        logManager.playStory = false;

        // 2. ȭ���� �ٽ� ��� (���̵� ��)
        elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        isFading = false;
    }
}
