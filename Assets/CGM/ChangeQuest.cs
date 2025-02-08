using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeQuest : MonoBehaviour
{
    public GameObject[] objects; // 4���� UI ������Ʈ �迭
    private int currentIndex = 0; // ���� Ȱ��ȭ�� ���� �ε���

    void Start()
    {
        UpdateObjects();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchObjects();
        }
    }

    void SwitchObjects()
    {
        // ���� Ȱ��ȭ�� �� ���� ������Ʈ�� ��Ȱ��ȭ
        objects[currentIndex].SetActive(false);
        objects[(currentIndex + 1) % 4].SetActive(false);

        // ���� �� ���� ������Ʈ�� Ȱ��ȭ
        currentIndex = (currentIndex + 1) % 4;
        int nextIndex = (currentIndex + 1) % 4;

        // 3��, 4�� �� 4��, 1���� �ǵ��� ���� ���� + Hierarchy ���� ����
        if (currentIndex == 3)
        {
            objects[3].SetActive(true); // 4�� Ȱ��ȭ
            objects[0].SetActive(true); // 1�� Ȱ��ȭ

            // Hierarchy���� 4���� 1������ ���� ������ ����
            objects[3].transform.SetAsLastSibling(); // 4���� ���������� �̵�
            objects[0].transform.SetAsLastSibling(); // 1���� ���������� �̵� (4�� ���� ��ġ)
        }
        else
        {
            objects[currentIndex].SetActive(true);
            objects[nextIndex].SetActive(true);

            // Hierarchy���� �ùٸ� ������ UI ����
            objects[currentIndex].transform.SetAsLastSibling();
            objects[nextIndex].transform.SetAsLastSibling();
        }
    }

    void UpdateObjects()
    {
        // �ʱ� ����: 1��, 2���� Ȱ��ȭ
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i < 2);
        }
    }
}