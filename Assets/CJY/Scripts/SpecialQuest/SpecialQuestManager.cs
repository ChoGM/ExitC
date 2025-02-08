using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialQuestManager : MonoBehaviour
{
    public List<SpecialQuest> quests; // �� 12���� ����Ʈ�� �����ϴ� ����Ʈ

    // ���º� + �ܰ躰 ����Ʈ�� ������ Dictionary
    private Dictionary<FactionType, Dictionary<int, SpecialQuest>> questDictionary;

    private SpecialQuest NowPublicAuthorityQuest;
    private SpecialQuest NowRevolutionaryArmyQuest;
    private SpecialQuest NowCultQuest;
    private SpecialQuest NowCrimeSyndicateQuest;

    public Text PublicAuthorityText1; // ���Ƿ� ù ��° ����Ʈ �ؽ�Ʈ
    public Text PublicAuthorityText2;
    public Text PublicAuthorityText3;

    public Text RevolutionaryArmyText1; // ���� ù ��° ����Ʈ �ؽ�Ʈ
    public Text RevolutionaryArmyText2;
    public Text RevolutionaryArmyText3;

    public Text CultText1; // ���̺� ù ��° ����Ʈ �ؽ�Ʈ
    public Text CultText2;
    public Text CultText3;

    public Text CrimeSyndicateText1; // �������� ù ��° ����Ʈ �ؽ�Ʈ
    public Text CrimeSyndicateText2;
    public Text CrimeSyndicateText3;

    private void Start()
    {
        // ���� ������ �ε� �� ���º� ����Ʈ�� ����
        NowPublicAuthorityQuest = GetQuest(FactionType.PublicAuthority, Datamanager.Instance.data.PublicAuthority_Step);
        NowRevolutionaryArmyQuest = GetQuest(FactionType.RevolutionaryArmy, Datamanager.Instance.data.RevolutionaryArmy_Step);
        NowCultQuest = GetQuest(FactionType.Cult, Datamanager.Instance.data.Cult_Step);
        NowCrimeSyndicateQuest = GetQuest(FactionType.CrimeSyndicate, Datamanager.Instance.data.CrimeSyndicate_Step);

        // ����Ʈ ���� ��� (����׿�)
        Debug.Log(NowPublicAuthorityQuest.questTitle);  // ����Ʈ ����
        Debug.Log(NowPublicAuthorityQuest.questDescription1);  // ����Ʈ ����

        //���º� ����Ʈ ������
        UpdatePublicText(NowPublicAuthorityQuest, PublicAuthorityText1, PublicAuthorityText2, PublicAuthorityText3);
        //UpdateRevolutionaryArmyText(NowRevolutionaryArmyQuest, RevolutionaryArmyText1, RevolutionaryArmyText2, RevolutionaryArmyText3);
        //UpdateCultText(NowCultQuest, CultText1, CultText2, CultText3);
        //UpdateCrimeSyndicateText(NowCrimeSyndicateQuest, CrimeSyndicateText1, CrimeSyndicateText2, CrimeSyndicateText3);
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
            if (!questDictionary[quest.factionType].ContainsKey(quest.questStep))
            {
                questDictionary[quest.factionType][quest.questStep] = quest;
            }
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

    private void UpdatePublicText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1} \n �������  ({quest.countBlock_WeaponStore}/{quest.requiredBlock_WeaponStore})";
            }
            else
            {
                questOrderText1.text = $"{quest.questDescription1} \n������� ({quest.countBlock_WeaponStore}/{quest.requiredBlock_WeaponStore}) �ְŰ��� ({quest.countBlock_CleanHouse}/{quest.requiredBlock_CleanHouse})";
            }

            questOrderText2.text = $"{quest.questDescription2} ({quest.floorLimitcount}/{quest.floorLimit})";
        }
    }

    private void UpdateRevolutionaryArmyText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1} \n ���� �ν�  ({quest.statusCount_sentiment})";
            }

            questOrderText2.text = $"{quest.questDescription2} \n ���� ������� ���� ({quest.countBlock_WeaponStore})";
        }
    }

    private void UpdateCultText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1} \n ���� û�ᵵ  ({quest.statusCount_clear})";
            }
            else
            {
                questOrderText1.text = $"{quest.questDescription1} \n ���� û�ᵵ  ({quest.statusCount_clear}) ���� ���� ({quest.statusCount_trouble})";
            }

            questOrderText2.text = $"{quest.questDescription2} ({quest.floorLimitcount}/{quest.floorLimit})";
        }
    }

    private void UpdateCrimeSyndicateText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1} \n ���� ����  ({quest.statusCount_trouble})";
            }

            questOrderText2.text = $"{quest.questDescription2}  ({quest.countBlock_CleanHouse_Store}/{quest.requiredBlock_CleanHouse_Store})";
        }
    }
}