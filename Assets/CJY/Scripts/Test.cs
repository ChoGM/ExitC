using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private QuestManager questManager;  // QuestManager �ν��Ͻ��� ���� ����

    void Start()
    {
        // QuestManager ��ü�� ã�� �ش� ������Ʈ�� ������
        questManager = GameObject.FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("QuestManager�� ������ ã�� �� �����ϴ�.");  // QuestManager�� ���� ��� ���� �α� ���
        }
    }

    void Update()
    {
        
    }

    // ������Ʈ�� �ı��� �� �ڵ����� ȣ��Ǵ� �Լ�
    void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� QuestManager�� OnObjectDestroyed ȣ��
        if (questManager != null)
        {
            questManager.OnObjectDestroyed(gameObject);  // ���� ������Ʈ�� ����
        }
    }
}
