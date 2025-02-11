using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBlock : MonoBehaviour
{
    public List<Text> objectCountTexts; // �±׺��� ���� UI �Ҵ�
    public List<string> tagsToTrack; // Ȯ���� �±� ����Ʈ

    private int count;

    void Start()
    {
        UpdateAllTagCounts(); // �ʱ� UI ������Ʈ
        count = 0;
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
                objectCountTexts[i].text = $" X {count}";
            }
        }
    }

    // Ư�� �±׸� ���� ������Ʈ ���� ���
    public int CountObjectsWithTag(string tagName)
    {
        Tetris_Tetrimino[] tetriminos = FindObjectsOfType<Tetris_Tetrimino>(); // ��� Tetris_Tetrimino ã��
        Debug.Log($"Tetris_Tetrimino ����: {tetriminos.Length}");

        int count = 0;

        foreach (Tetris_Tetrimino tetrimino in tetriminos)
        {
            if (tetrimino.isLocked) // isLocked�� true�� ��츸 Ȯ��
            {
                Debug.Log($"Tetris_Tetrimino {tetrimino.gameObject.name}�� isLocked�� true�̹Ƿ� Ȯ��");

                foreach (Transform child in tetrimino.transform) // �ڽ� ������Ʈ Ȯ��
                {
                    if (child.CompareTag(tagName)) // Ư�� �±׿� ��ġ�ϸ� ī��Ʈ ����
                    {
                        count++;
                    }
                }
            }
        }

        Debug.Log($"�±� [{tagName}]�� ���� �ڽ� ������Ʈ ����: {count}");
        return count;
    }

}