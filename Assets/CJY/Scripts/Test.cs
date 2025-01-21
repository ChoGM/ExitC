using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private QuestManager questManager;  // QuestManager 인스턴스에 대한 참조

    void Start()
    {
        // QuestManager 객체를 찾고 해당 컴포넌트를 가져옴
        questManager = GameObject.FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("QuestManager를 씬에서 찾을 수 없습니다.");  // QuestManager가 없을 경우 에러 로그 출력
        }
    }

    void Update()
    {

    }

    // 오브젝트가 파괴될 때 자동으로 호출되는 함수
    void OnDestroy()
    {
        // questManager가 null인지 확인하고 OnObjectDestroyed 호출
        if (questManager != null && gameObject != null)
        {
            // 현재 오브젝트를 전달
            questManager.OnObjectDestroyed(gameObject);
        }
        else
        {
            Debug.LogWarning("OnDestroy 호출 시 QuestManager가 null이거나 객체가 존재하지 않습니다.");
        }
    }
}
