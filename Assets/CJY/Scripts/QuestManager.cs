using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [Header("Quest Settings")]
    public List<QuestScrip> questScripts; // �⺻ ����Ʈ ������ ����Ʈ (��ũ���ͺ� ������Ʈ)
    private List<QuestScrip> activeQuests = new List<QuestScrip>(); // �������� ���յ� ����Ʈ ����Ʈ

    public Text questText1; // ù ��° ����Ʈ �ؽ�Ʈ
    public Text questText2; // �� ��° ����Ʈ �ؽ�Ʈ
    public Text questText3; // �� ��° ����Ʈ �ؽ�Ʈ
    public Text questText1_1; // ù ��° ����Ʈ ���� �ؽ�Ʈ
    public Text questText2_1; // �� ��° ����Ʈ ���� �ؽ�Ʈ
    public Text questText3_1; // �� ��° ����Ʈ ���� �ؽ�Ʈ

    private void Start()
    {
        InitializeQuests(); // ����Ʈ �ʱ�ȭ
        GenerateDynamicQuests(); // �������� ���յ� ����Ʈ ����
        UpdateQuestUI();    // UI �ʱ�ȭ
    }

    private void Update()
    {
        foreach (QuestScrip quest in activeQuests)
        {
            if (!quest.isCompleted && quest.questType == QuestType1.Count)
            {
                // �±װ� ��ġ�ϴ� ������Ʈ�� ������ Ȯ��
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(quest.tag);
                quest.currentCount = objectsWithTag.Length;

                if (quest.currentCount >= quest.requiredCount)
                {
                    quest.isCompleted = true;
                    Debug.Log($"{quest.title} quest completed!");
                }
            }
        }

        // Tab Ű�� ����Ʈ ���� ȸ��
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            RotateQuests();
        }

        UpdateQuestUI();
    }

    private void InitializeQuests()
    {
        // ��� ����Ʈ �ʱ�ȭ
        foreach (QuestScrip quest in questScripts)
        {
            quest.ResetQuest();
        }
    }

    private void GenerateDynamicQuests()
    {
        activeQuests.Clear();

        foreach (QuestScrip baseQuest in questScripts)
        {
            // �������� ������ ����Ʈ �ν��Ͻ� ����
            QuestScrip dynamicQuest = ScriptableObject.CreateInstance<QuestScrip>();

            // ���� ����Ʈ ������ ����
            dynamicQuest.title = baseQuest.title;
            dynamicQuest.questType = baseQuest.questType;
            dynamicQuest.questOrder = baseQuest.questOrder;
            dynamicQuest.tag = baseQuest.tag; // �±״� �״�� ����

            // Destroy Ÿ���� ��쿡�� requiredCount�� �������� ����
            if (baseQuest.questType == QuestType1.Destroy)
            {
                dynamicQuest.requiredCount = Random.Range(3, 10); // ���� ���� ���� (��: 3 ~ 10)
            }
            else
            {
                dynamicQuest.requiredCount = baseQuest.requiredCount; // Count Ÿ���� ���� �� ����
            }

            dynamicQuest.ResetQuest(); // �ʱ�ȭ

            activeQuests.Add(dynamicQuest);
        }

        Debug.Log($"�� {activeQuests.Count}���� ���� ����Ʈ�� �����Ǿ����ϴ�.");

        // UI ������Ʈ ȣ��
        UpdateQuestUI();
    }


    public void OnObjectDestroyed(string objectTag)
    {
        foreach (QuestScrip quest in activeQuests)
        {
            // �±װ� ��ġ�ϰ� �ı� ����Ʈ�̸� �Ϸ���� ���� ���
            if (quest.tag == objectTag && quest.questType == QuestType1.Destroy && !quest.isCompleted)
            {
                quest.IncrementCount(); // ī��Ʈ ����
                Debug.Log($"[Destroy] {objectTag} destroyed. Progress: {quest.currentCount}/{quest.requiredCount}");

                // ����Ʈ �Ϸ� Ȯ��
                if (quest.currentCount >= quest.requiredCount)
                {
                    quest.isCompleted = true;
                    Debug.Log($"����Ʈ �Ϸ�: {quest.title}");
                }
            }
        }

        // UI ������Ʈ ȣ��
        UpdateQuestUI();
    }


    private void UpdateQuestUI()
    {
        // activeQuests ����Ʈ�� ��ȸ�ϸ� UI ������Ʈ
        for (int i = 0; i < activeQuests.Count; i++)
        {
            QuestScrip quest = activeQuests[i];

            if (i == 0) UpdateQuestText(quest, questText1, questText1_1);
            else if (i == 1) UpdateQuestText(quest, questText2, questText2_1);
            else if (i == 2) UpdateQuestText(quest, questText3, questText3_1);
        }

        // ������ ������ ��Ȱ��ȭ
        if (activeQuests.Count < 3)
        {
            if (questText1 != null && activeQuests.Count < 1) questText1.gameObject.SetActive(false);
            if (questText2 != null && activeQuests.Count < 2) questText2.gameObject.SetActive(false);
            if (questText3 != null && activeQuests.Count < 3) questText3.gameObject.SetActive(false);
        }
    }


    private void UpdateQuestText(QuestScrip quest, Text questText, Text questOrderText)
    {
        if (quest != null && questText != null && questOrderText != null)
        {
            questText.text = $"{quest.title} ({quest.currentCount}/{quest.requiredCount})";
            questOrderText.text = "QUEST. " + quest.questOrder;
            questText.gameObject.SetActive(true);
            questOrderText.gameObject.SetActive(true);
        }
        else if (questText != null && questOrderText != null)
        {
            questText.gameObject.SetActive(false);
            questOrderText.gameObject.SetActive(false);
        }
    }

    private void RotateQuests()
    {
        if (activeQuests.Count == 0) return;

        QuestScrip firstQuest = activeQuests[0];
        activeQuests.RemoveAt(0);
        activeQuests.Add(firstQuest);

        Debug.Log("����Ʈ ������ �����߽��ϴ�.");
        UpdateQuestUI();
    }

    private string GetRandomTag()
    {
        // ���� �±׸� ��ȯ (����� �䱸�� ���� ���� ����)
        string[] possibleTags = { "CleanHouse", "WeaponStore", "Crime", "Hospital", "TrashHouse", "Store" };
        return possibleTags[Random.Range(0, possibleTags.Length)];
    }
}
