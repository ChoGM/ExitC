using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialQuestManager : MonoBehaviour
{
    public List<SpecialQuest> quests; // �� 12���� ����Ʈ�� �����ϴ� ����Ʈ

    public Data gameData; // ���� �����͸� �����ϴ� ��ü
    public Datamanager datamanager;

    // ���º� + �ܰ躰 ����Ʈ�� ������ Dictionary
    private Dictionary<FactionType, Dictionary<int, SpecialQuest>> questDictionary;

    public Text questText1; // ù ��° ����Ʈ �ؽ�Ʈ
    public Text questText2; // �� ��° ����Ʈ �ؽ�Ʈ
    public Text questText1_1; // ù ��° ����Ʈ ���� �ؽ�Ʈ
    public Text questText2_1; // �� ��° ����Ʈ ���� �ؽ�Ʈ

    private void Start()
    {
        Datamanager.Instance.LoadGameData();

        Debug.Log("����Ʈ �ܰ�: " + Datamanager.Instance.data.PublicAuthority_Step);

    }

    private void Awake()
    {
        InitializeQuestDictionary(); // ���� ���� �� Dictionary�� �ʱ�ȭ
    }

    private void InitializeQuestDictionary()
    {
        questDictionary = new Dictionary<FactionType, Dictionary<int, SpecialQuest>>(); // Dictionary ��ü ����

        // ��� ����Ʈ�� �ϳ��� ��ȸ�ϸ鼭 Dictionary�� �߰�
        foreach (var quest in quests)
        {
            // �ش� ���� Ÿ���� Dictionary�� ���� ��� ���� �߰�
            if (!questDictionary.ContainsKey(quest.factionType))
            {
                questDictionary[quest.factionType] = new Dictionary<int, SpecialQuest>(); // ���� Ÿ�Կ� ���� Dictionary ����
            }

            // ���� Ÿ�� ������ �ش� �ܰ迡 ���� ����Ʈ�� ����
            questDictionary[quest.factionType][quest.questStep] = quest;
        }
    }

    // Ư�� ���°� �ܰ迡 �´� ����Ʈ�� �������� �Լ�
    public SpecialQuest GetQuest(FactionType faction, int step)
    {
        // Dictionary���� �ش� ���� Ÿ���� �����ϴ��� Ȯ��
        if (questDictionary.TryGetValue(faction, out var stepDict))
        {
            // ���� ������ �ش� �ܰ��� ����Ʈ�� �����ϴ��� Ȯ�� �� ��ȯ
            if (stepDict.TryGetValue(step, out var quest))
            {
                return quest; // �ش��ϴ� ����Ʈ ��ȯ
            }
        }
        return null; // ����Ʈ�� �������� ������ null ��ȯ
    }
}
