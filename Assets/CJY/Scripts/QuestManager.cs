using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Text questUIText;  // ����Ʈ ������ ������ UI �ؽ�Ʈ
    private List<Quest> quests = new List<Quest>();  // ����Ʈ ����Ʈ

    void Start()
    {
        // ���÷� �� ���� ����Ʈ �߰� (�ı� ����Ʈ�� ī��Ʈ ����Ʈ)
        AddQuest("Count 5 ġ��ü�", "ġ��ü�", 5, QuestType.Count);
        AddQuest("Count 3 ��������", "��������", 3, QuestType.Count);
        AddQuest("Destroy 4 ġ��ü�", "ġ��ü�", 4, QuestType.Destroy);

        // UI ������Ʈ
        UpdateQuestUI();
    }

    void Update()
    {
        // Update���� Ư�� �±׸� ���� ������Ʈ ���� �̼� ������ �����ϴ��� Ȯ��
        CompleteQuestIfTagCountMet("ġ��ü�", 5); // "Clear" �±׸� ���� ������Ʈ�� 5���� �� ����Ʈ �Ϸ�
    }

    // ����Ʈ �߰� �Լ�
    public void AddQuest(string title, string tag, int requiredCount, QuestType questType)
    {
        quests.Add(new Quest(title, tag, requiredCount, questType));
        UpdateQuestUI();
    }

    // Ư�� ������Ʈ�� �ı��� �� ȣ��Ǵ� �Լ� (�ı� ����Ʈ���� �ش�)
    public void OnObjectDestroyed(GameObject destroyedObject)
    {
        string objectTag = destroyedObject.tag;

        // �ı��� ������Ʈ�� �±׸� ���� ����Ʈ�� Ȯ���ϰ� ī��Ʈ ����
        foreach (Quest quest in quests)
        {
            if (quest.tag == objectTag && !quest.isCompleted && quest.questType == QuestType.Destroy)
            {
                quest.IncrementCount();
                Debug.Log($"{objectTag} destroyed. Count: {quest.currentCount}/{quest.requiredCount}");

                // ����Ʈ �Ϸ� ���� Ȯ��
                CompleteQuestIfTagCountMet(objectTag, quest.requiredCount);
            }
        }

        // UI ������Ʈ
        UpdateQuestUI();
    }

    // Ư�� �±׸� ���� ������Ʈ ���� ��ǥġ�� �����ϸ� ����Ʈ �Ϸ� ó�� (ī��Ʈ ����Ʈ���� �ش�)
    public void CompleteQuestIfTagCountMet(string tag, int requiredCount)
    {
        // �±׸� ���� ������Ʈ �迭 ��������
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // ���� �±׸� ���� ������Ʈ ���� ����� ������ϱ�
        Debug.Log($"Found {objectsWithTag.Length} objects with tag '{tag}'");

        // ī��Ʈ ����Ʈ�� �ش��ϴ� ������Ʈ ���� ��ǥġ�� �����ϸ� ����Ʈ �Ϸ�
        foreach (Quest quest in quests)
        {
            if (quest.tag == tag && !quest.isCompleted)
            {
                if (quest.questType == QuestType.Count)
                {
                    // ������Ʈ �� ����Ʈ�� currentCount�� ������Ʈ
                    quest.currentCount = objectsWithTag.Length;

                    // ��ǥġ�� �޼��ߴ��� Ȯ��
                    if (quest.currentCount >= requiredCount)
                    {
                        CompleteQuest(quest.title);
                    }
                }
            }
        }

        // UI ������Ʈ
        UpdateQuestUI();
    }

    // ����Ʈ �Ϸ� �Լ�
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

    // UI�� ����Ʈ ����Ʈ ������Ʈ �Լ�
    private void UpdateQuestUI()
    {
        string questText = "";

        foreach (Quest quest in quests)
        {
            if (quest.isCompleted)
            {
                questText += "[Completed] " + quest.title + "\n";  // �Ϸ�� ����Ʈ ǥ��
            }
            else
            {
                questText += "[In Progress] " + quest.title + $" ({quest.currentCount}/{quest.requiredCount})\n";  // ���� ���� ����Ʈ ǥ��
            }
        }
        questUIText.text = questText;
    }
}
