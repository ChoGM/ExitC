using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBlock : MonoBehaviour
{
    public Text objectCountText; // ��� �±��� ������ ǥ���� �ϳ��� �ؽ�Ʈ
    public List<string> tagsToTrack; // Ȯ���� �±� ����Ʈ

    void Start()
    {
        UpdateAllTagCounts(); // �ʱ� UI ������Ʈ
    }

    void Update()
    {
        // �� �����Ӹ��� ������Ʈ���� �ʰ�, ���� �ֱ⸶�� ����
        if (Time.frameCount % 30 == 0) // 30�����Ӹ��� ����
        {
            UpdateAllTagCounts();
        }
    }

    // ��� �±��� ������Ʈ ���� ������Ʈ
    private void UpdateAllTagCounts()
    {
        string updatedText = ""; // �ؽ�Ʈ�� ������ ����

        foreach (var tag in tagsToTrack)
        {
            int count = CountObjectsWithTag(tag);
            updatedText += $"{tag}: {count}��\n"; // �� �پ� �߰�
        }

        objectCountText.text = updatedText; // �ؽ�Ʈ UI ����
    }

    // Ư�� �±׸� ���� ������Ʈ ���� ���
    public int CountObjectsWithTag(string tagName)
    {
        return GameObject.FindGameObjectsWithTag(tagName).Length;
    }
}
