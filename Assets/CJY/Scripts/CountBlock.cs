using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBlock : MonoBehaviour
{
    public List<Text> objectCountTexts; // �±׺��� ���� UI �Ҵ�
    public List<string> tagsToTrack; // Ȯ���� �±� ����Ʈ

    void Start()
    {
        UpdateAllTagCounts(); // �ʱ� UI ������Ʈ
    }

    void Update()
    {
        if (Time.frameCount % 30 == 0) // 30�����Ӹ��� ����
        {
            UpdateAllTagCounts();
        }
    }

    // ��� �±��� ������Ʈ ���� ������Ʈ
    private void UpdateAllTagCounts()
    {
        for (int i = 0; i < tagsToTrack.Count; i++)
        {
            if (i < objectCountTexts.Count) // UI ��Ұ� ������ ��� ���
            {
                int count = CountObjectsWithTag(tagsToTrack[i]);
                objectCountTexts[i].text = $"{tagsToTrack[i]}: X {count}��";
            }
        }
    }

    // Ư�� �±׸� ���� ������Ʈ ���� ���
    public int CountObjectsWithTag(string tagName)
    {
        return GameObject.FindGameObjectsWithTag(tagName).Length;
    }
}
