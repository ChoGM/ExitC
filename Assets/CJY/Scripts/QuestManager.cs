using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Text questText1; // ù ��° ����Ʈ �ؽ�Ʈ
    public Text questText2; // �� ��° ����Ʈ �ؽ�Ʈ
    public Text questText3; // �� ��° ����Ʈ �ؽ�Ʈ
    public Text questText1_1; // ù ��° ����Ʈ ���� �ؽ�Ʈ
    public Text questText2_1; // �� ��° ����Ʈ ���� �ؽ�Ʈ
    public Text questText3_1; // �� ��° ����Ʈ ���� �ؽ�Ʈ

    private List<Quest> quests = new List<Quest>();  // ����Ʈ ����Ʈ

    void Start()
    {
        AddQuest("Count 5 ġ��ü�", "ġ��ü�", 5, QuestType.Count, 1);
        AddQuest("Count 3 ��������", "��������", 3, QuestType.Count, 2);
        AddQuest("Destroy 4 ġ��ü�", "ġ��ü�", 4, QuestType.Destroy, 3);
        AddQuest("Destroy 4 ��������", "��������", 2, QuestType.Destroy, 4);

        UpdateQuestUI();
    }

    void Update()
    {
        CompleteQuestIfTagCountMet("ġ��ü�", 5);
        CompleteQuestIfTagCountMet("��������", 3);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            RotateQuests();
        }
    }

    public void AddQuest(string title, string tag, int requiredCount, QuestType questType, int questOrder)
    {
        quests.Add(new Quest(title, tag, requiredCount, questType, questOrder));
        UpdateQuestUI();
    }

    public void OnObjectDestroyed(GameObject destroyedObject)
    {
        string objectTag = destroyedObject.tag;

        foreach (Quest quest in quests)
        {
            if (quest.tag == objectTag && !quest.isCompleted && quest.questType == QuestType.Destroy)
            {
                quest.IncrementCount();
                Debug.Log($"{objectTag} destroyed. Count: {quest.currentCount}/{quest.requiredCount}");
                CompleteQuestIfTagCountMet(objectTag, quest.requiredCount);
            }
        }

        UpdateQuestUI();
    }

    public void CompleteQuestIfTagCountMet(string tag, int requiredCount)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (Quest quest in quests)
        {
            if (quest.tag == tag && !quest.isCompleted)
            {
                if (quest.questType == QuestType.Count)
                {
                    quest.currentCount = objectsWithTag.Length;

                    if (quest.currentCount >= requiredCount)
                    {
                        CompleteQuest(quest.title);
                    }
                }
            }
        }

        UpdateQuestUI();
    }

    public void CompleteQuest(string title)
    {
        foreach (Quest quest in quests)
        {
            if (quest.title == title && !quest.isCompleted)
            {
                quest.isCompleted = true;
                Debug.Log($"{title} quest completed!");
                break;
            }
        }

        UpdateQuestUI();
    }

    private void UpdateQuestUI()
    {
        UpdateQuestText(quests.Count > 0 ? quests[0] : null, questText1, questText1_1);
        UpdateQuestText(quests.Count > 1 ? quests[1] : null, questText2, questText2_1);
        UpdateQuestText(quests.Count > 2 ? quests[2] : null, questText3, questText3_1);
    }

    private void UpdateQuestText(Quest quest, Text questText, Text questOrderText)
    {
        if (quest != null && questText != null && questOrderText != null)
        {
            questText.text = GetQuestText(quest);
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

    private string GetQuestText(Quest quest)
    {
        string statusText = quest.isCompleted ? "[Completed] " : "[In Progress] ";
        return statusText + quest.title + $" ({quest.currentCount}/{quest.requiredCount})";
    }

    private void RotateQuests()
    {
        if (quests.Count == 0) return;

        Quest firstQuest = quests[0];
        quests.RemoveAt(0);
        quests.Add(firstQuest);

        Debug.Log("����Ʈ ������ �����߽��ϴ�.");
        UpdateQuestUI();
    }
}
