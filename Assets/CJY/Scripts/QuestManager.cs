using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    // UI �ؽ�Ʈ�� ����Ʈ ���¸� ������
    public Text questUIText;

    // ����Ʈ ����
    private string currentQuest;
    private bool questCompleted;

    void Start()
    {
        // ����Ʈ ���� �� �ʱ�ȭ
        currentQuest = "Collect 5 apples";
        questCompleted = false;
        UpdateQuestUI();
    }

    // ����Ʈ�� ������Ʈ�ϴ� �Լ�
    public void UpdateQuest(string newQuest)
    {
        currentQuest = newQuest;
        questCompleted = false;
        UpdateQuestUI();
    }

    // ����Ʈ�� �Ϸ����� �� ȣ���ϴ� �Լ�
    public void CompleteQuest()
    {
        questCompleted = true;
        UpdateQuestUI();
    }

    // UI�� ����Ʈ ���¸� ������Ʈ�ϴ� �Լ�
    private void UpdateQuestUI()
    {
        if (questCompleted)
        {
            questUIText.text = "Quest Completed: " + currentQuest;
        }
        else
        {
            questUIText.text = "Current Quest: " + currentQuest;
        }
    }
}
