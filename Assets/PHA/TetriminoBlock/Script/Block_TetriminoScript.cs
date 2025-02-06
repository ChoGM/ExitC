using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_TetriminoScript : MonoBehaviour
{
    private QuestManager questManager; // QuestManager �ν��Ͻ��� ���� ����

    public string GetTag()
    {
        return this.tag;
    }

    void Start()
    {
        // QuestManager ��ü�� ã�� �ش� ������Ʈ�� ������
        questManager = GameObject.FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("QuestManager�� ������ ã�� �� �����ϴ�."); // QuestManager�� ���� ��� ���� �α� ���
        }
    }

    Block_TetriminoScript() 
    {
        // �����̴��� �� ���� ����
    }

    ~Block_TetriminoScript() 
    {
        //�����̴��� �� ���� ����
    }

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
            Debug.LogWarning("OnDestroy ȣ�� �� QuestManager�� null�̰ų� ��ü�� �������� �ʽ��ϴ�.");
        }
    }
}
