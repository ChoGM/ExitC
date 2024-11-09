using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestType
{
    Destroy,  // �ı��ؾ� �ϴ� ����Ʈ
    Count     // Ư�� ����ŭ ������Ʈ�� �����ؾ� �ϴ� ����Ʈ
}

[System.Serializable]
public class Quest
{
    public string title;  // ����Ʈ �̸�
    public string tag;    // ����Ʈ�� �ش��ϴ� �±�
    public int requiredCount;  // ��ǥ ������Ʈ �� (�ı� �Ǵ� ����)
    public bool isCompleted;   // ����Ʈ �Ϸ� ����
    public int currentCount;   // ���� �ı��� ������Ʈ ��
    public QuestType questType; // ����Ʈ Ÿ�� (�ı� �Ǵ� ī��Ʈ)

    public Quest(string title, string tag, int requiredCount, QuestType questType)
    {
        this.title = title;
        this.tag = tag;
        this.requiredCount = requiredCount;
        this.isCompleted = false;
        this.currentCount = 0;
        this.questType = questType;
    }

    // �ı� ����Ʈ�� ī��Ʈ�� ������Ű�� ����Ʈ �Ϸ� ���� Ȯ��
    public void IncrementCount()
    {
        if (questType == QuestType.Destroy)
        {
            currentCount++;
        }
        if (currentCount >= requiredCount)
        {
            isCompleted = true;
            Debug.Log($"{title} quest completed!");
        }
    }
}
