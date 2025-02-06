using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingQuest : MonoBehaviour
{
    public enum EndingType
    {
        NormalEnding,
        Ending1,
        Ending2
    }

    // ����Ʈ ���� ����
    private Dictionary<string, bool> questStatus = new Dictionary<string, bool>();
    private string currentActiveQuest = ""; // ���� Ȱ��ȭ�� ����Ʈ

    // UI ������Ʈ ����
    public Text activeQuestText; // ���� Ȱ��ȭ�� ����Ʈ ǥ��

    private void Start()
    {
        // ��� ����Ʈ �ʱ�ȭ
        InitializeQuests();

        // �ʱ� ����Ʈ Ȱ��ȭ
        ActivateQuest("1-1");

        // UI �ʱ�ȭ
        UpdateUI();
    }

    private void InitializeQuests()
    {
        // ���� 1 ���� ����Ʈ
        questStatus["1-1"] = false;
        questStatus["1-2"] = false;
        questStatus["1-3"] = false;

        // ���� 2 ���� ����Ʈ
        questStatus["2-1"] = false;
        questStatus["2-2"] = false;
        questStatus["2-3"] = false;
    }

    private void ActivateQuest(string questId)
    {
        if (!questStatus.ContainsKey(questId))
        {
            Debug.LogWarning($"����Ʈ {questId}�� �������� �ʽ��ϴ�.");
            return;
        }

        currentActiveQuest = questId; // Ȱ��ȭ�� ����Ʈ ����
        //Debug.Log($"����Ʈ {questId}�� Ȱ��ȭ�Ǿ����ϴ�!");
        UpdateUI();
    }

    public void CompleteQuest(string questId)
    {
        if (!questStatus.ContainsKey(questId))
        {
            //Debug.LogWarning($"����Ʈ {questId}�� �������� �ʽ��ϴ�.");
            return;
        }

        if (currentActiveQuest != questId)
        {
            //Debug.LogWarning($"���� Ȱ��ȭ�� ����Ʈ�� �ƴմϴ�: {questId}");
            return;
        }

        // �Ϸ� ó��
        questStatus[questId] = true;
        Debug.Log($"����Ʈ {questId} �Ϸ�!");

        // ���� ����Ʈ Ȱ��ȭ ó��
        UnlockNextQuests(questId);
    }

    private void UnlockNextQuests(string completedQuestId)
    {
        switch (completedQuestId)
        {
            case "1-1":
                ActivateQuest("1-2");
                break;
            case "1-2":
                ActivateQuest("1-3");
                break;
            case "1-3":
                //Debug.Log("���� 1 ����Ʈ �Ϸ�!");
                currentActiveQuest = ""; // ����Ʈ ����
                break;
            case "2-1":
                ActivateQuest("2-2");
                break;
            case "2-2":
                ActivateQuest("2-3");
                break;
            case "2-3":
                //Debug.Log("���� 2 ����Ʈ �Ϸ�!");
                currentActiveQuest = ""; // ����Ʈ ����
                break;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (!string.IsNullOrEmpty(currentActiveQuest))
        {
            activeQuestText.text = $"���� ����Ʈ: {currentActiveQuest}";
        }
        else
        {
            activeQuestText.text = "��� ����Ʈ �Ϸ�!";
        }
    }

    private void Update()
    {
        // 1�� Ű�� ������ �� 1-1 ����Ʈ �Ϸ�
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentActiveQuest == "1-1")
        {
            CompleteQuest("1-1");
        }

        // 2�� Ű�� ������ �� 1-2 ����Ʈ �Ϸ�
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentActiveQuest == "1-2")
        {
            CompleteQuest("1-2");
        }

        // 3�� Ű�� ������ �� 1-3 ����Ʈ �Ϸ�
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentActiveQuest == "1-3")
        {
            CompleteQuest("1-3");
        }
    }
}
