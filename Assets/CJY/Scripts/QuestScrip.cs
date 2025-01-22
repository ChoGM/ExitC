using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType1
{
    Destroy,  // �ı��ؾ� �ϴ� ����Ʈ
    Count     // Ư�� ����ŭ ������Ʈ�� �����ؾ� �ϴ� ����Ʈ
}

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/Create New Quest")]
public class QuestScrip : ScriptableObject
{
    [Header("Quest Information")]
    public string title;          // ����Ʈ �̸�
    public string tag;            // ����Ʈ�� �ش��ϴ� �±�
    public int requiredCount;     // ��ǥ ������Ʈ �� (�ı� �Ǵ� ����)
    public QuestType1 questType;   // ����Ʈ Ÿ�� (�ı� �Ǵ� ī��Ʈ)
    public string questOrder;        // ����Ʈ ����

    [Header("Quest Progress")]
    public bool isCompleted;      // ����Ʈ �Ϸ� ����
    public int currentCount;      // ���� �ı��� ������Ʈ ��

    // �ı� ����Ʈ�� ī��Ʈ�� ������Ű�� ����Ʈ �Ϸ� ���� Ȯ��
    public void IncrementCount()
    {
        if (questType == QuestType1.Destroy)
        {
            currentCount++;
        }
        if (currentCount >= requiredCount)
        {
            isCompleted = true;
            Debug.Log($"{title} quest completed!");
        }
    }

    // ����Ʈ �ʱ�ȭ �޼���
    public void ResetQuest()
    {
        currentCount = 0;
        isCompleted = false;
    }
}
