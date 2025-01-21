using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetriminoStatus : MonoBehaviour
{
    // ������ �±� ����Ʈ
    public string[] tagsToTrack = { "TAG_A", "TAG_B", "TAG_C", "TAG_D", "TAG_E", "TAG_F" };

    // �±׺� ī��Ʈ�� Inspector���� ���� Ȯ���� �� �ֵ��� public ������ ����
    private float TAG_A_Count;
    private float TAG_B_Count;
    private float TAG_C_Count;
    private float TAG_D_Count;
    private float TAG_E_Count;
    private float TAG_F_Count;

    public float TAG_Count_Mid;
    public float TAG_Count_X;

    // �� �����̴��� ����� UI ���
    public Slider sliderA;
    public Slider sliderB;
    public Slider sliderC;

    // �±׺��� ���� ���� ������Ʈ�� �����ϴ� ��ųʸ�
    private Dictionary<string, List<GameObject>> taggedObjects = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, int> taggedObjectCounts = new Dictionary<string, int>();

    void Start()
    {
        sliderA.maxValue = TAG_Count_Mid * 2;
        sliderB.maxValue = TAG_Count_Mid * 2;
        sliderC.maxValue = TAG_Count_Mid * 2;

        // ��� �±׿� ���� �ʱ�ȭ
        foreach (string tag in tagsToTrack)
        {
            taggedObjects[tag] = new List<GameObject>();
            taggedObjectCounts[tag] = 0; // �ʱ� ������ 0���� ����
        }
    }

    void Update()
    {

        // �� �����Ӹ��� ī��Ʈ�� ������Ʈ
        UpdatePublicCounts();

        sliderA.value = TAG_Count_Mid + (TAG_A_Count * TAG_Count_X - TAG_B_Count * TAG_Count_X);
        sliderB.value = TAG_Count_Mid + (TAG_C_Count * TAG_Count_X - TAG_D_Count * TAG_Count_X);
        sliderC.value = TAG_Count_Mid + (TAG_E_Count * TAG_Count_X - TAG_F_Count * TAG_Count_X);

        // ������ ������Ʈ�� �����ϴ� �Լ� ȣ��
        CleanupTaggedObjects();
    }

    private void CleanupTaggedObjects()
    {
        foreach (string tag in tagsToTrack)
        {
            // ���� �±׿� ���� ������Ʈ ����Ʈ�� ��������
            List<GameObject> objects = taggedObjects[tag];

            // �����Ǿ��ų� null�� ������Ʈ�� ����Ʈ���� ����
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i] == null)
                {
                    objects.RemoveAt(i);
                    taggedObjectCounts[tag]--; // ī��Ʈ ����
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        foreach (string tag in tagsToTrack)
        {
            if (other.CompareTag(tag))
            {
                if (!taggedObjects[tag].Contains(other.gameObject))
                {
                    taggedObjects[tag].Add(other.gameObject);
                    taggedObjectCounts[tag]++;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (string tag in tagsToTrack)
        {
            if (other.CompareTag(tag))
            {
                if (taggedObjects[tag].Contains(other.gameObject))
                {
                    taggedObjects[tag].Remove(other.gameObject);
                    taggedObjectCounts[tag]--;
                }
            }
        }
    }

    // Inspector�� ����Ǵ� public ���� ������Ʈ
    private void UpdatePublicCounts()
    {
        TAG_A_Count = taggedObjectCounts.ContainsKey("TAG_A") ? taggedObjectCounts["TAG_A"] : 0;
        TAG_B_Count = taggedObjectCounts.ContainsKey("TAG_B") ? taggedObjectCounts["TAG_B"] : 0;
        TAG_C_Count = taggedObjectCounts.ContainsKey("TAG_C") ? taggedObjectCounts["TAG_C"] : 0;
        TAG_D_Count = taggedObjectCounts.ContainsKey("TAG_D") ? taggedObjectCounts["TAG_D"] : 0;
        TAG_E_Count = taggedObjectCounts.ContainsKey("TAG_E") ? taggedObjectCounts["TAG_E"] : 0;
        TAG_F_Count = taggedObjectCounts.ContainsKey("TAG_F") ? taggedObjectCounts["TAG_F"] : 0;
    }
}