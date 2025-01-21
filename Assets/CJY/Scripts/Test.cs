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
        // questManager�� null���� Ȯ���ϰ� OnObjectDestroyed ȣ��
        if (questManager != null && gameObject != null)
        {
            // ���� ������Ʈ�� ����
            questManager.OnObjectDestroyed(gameObject);
        }
        else
        {
            Debug.LogWarning("OnDestroy ȣ�� �� QuestManager�� null�̰ų� ��ü�� �������� �ʽ��ϴ�.");
        }
    }
}
