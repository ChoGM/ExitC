using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Block_TetriminoScript : MonoBehaviour
{
    private QuestManager questManager; // QuestManager �ν��Ͻ��� ���� ����

    void Start()
    {
        // QuestManager ��ü�� ã�� �ش� ������Ʈ�� ������
        questManager = GameObject.FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            UnityEngine.Debug.LogError("QuestManager�� ������ ã�� �� �����ϴ�."); // QuestManager�� ���� ��� ���� �α� ���
        }
    }

    // ������Ʈ�� �ı��� �� �ڵ����� ȣ��Ǵ� �Լ�
    void OnDestroy()
    {
        // QuestManager�� null���� Ȯ���ϰ� OnObjectDestroyed ȣ��
        if (questManager != null && gameObject != null)
        {
            // ���� ������Ʈ�� �±׸� �����Ͽ� ����Ʈ ����
            questManager.OnObjectDestroyed(gameObject.tag);
        }
        else
        {
            UnityEngine.Debug.LogWarning("OnDestroy ȣ�� �� QuestManager�� null�̰ų� ��ü�� �������� �ʽ��ϴ�.");
        }
    }
}
