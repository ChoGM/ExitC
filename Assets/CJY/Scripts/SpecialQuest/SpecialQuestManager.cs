using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialQuestManager : MonoBehaviour
{
    public List<SpecialQuest> quests; // �� 12���� ����Ʈ�� �����ϴ� ����Ʈ


    // ���º� + �ܰ躰 ����Ʈ�� ������ Dictionary
    private Dictionary<FactionType, Dictionary<int, SpecialQuest>> questDictionary;

    private TetriminoStatus tetriminoStatus;
    private CountBlock CountBlock;

    private SpecialQuest NowPublicAuthorityQuest;
    private SpecialQuest NowRevolutionaryArmyQuest;
    private SpecialQuest NowCultQuest;
    private SpecialQuest NowCrimeSyndicateQuest;

    public Text PublicAuthorityText1; // ���Ƿ� ù ��° ����Ʈ �ؽ�Ʈ
    public Text PublicAuthorityText2;
    public Text PublicAuthorityText3;
    public Text PublicCountWeaponText;
    public Text PublicCountCleanText;
    public GameObject PublicCleanImage;
    public Text PublicFloorText;

    public Text RevolutionaryArmyText1; // ���� ù ��° ����Ʈ �ؽ�Ʈ
    public Text RevolutionaryArmyText2;
    public Text RevolutionaryArmyText3;
    public Text RevolutionsentimentyText;
    public Text RevolutionWeaponText;

    public Text CultText1; // ���̺� ù ��° ����Ʈ �ؽ�Ʈ
    public Text CultText2;
    public Text CultText3;
    public Text CultCleanText;
    public Text CultTroubleText;
    public GameObject CultTroubleImage;
    public Text CultFloorText;

    public Text CrimeSyndicateText1; // �������� ù ��° ����Ʈ �ؽ�Ʈ
    public Text CrimeSyndicateText2;
    public Text CrimeSyndicateText3;
    public Text CrimeTroubleText;
    public Text CrimeCountText;

    private void Start()
    {
        foreach (var quest in quests)
        {
            quest.ResetQuest();
            //Debug.Log($"[QuestManager] {quest.name} �ʱ�ȭ��");
        }

        tetriminoStatus = FindObjectOfType<TetriminoStatus>();
        CountBlock = FindObjectOfType<CountBlock>();
        
        // ���� ������ �ε� �� ���º� ����Ʈ�� ����
        NowPublicAuthorityQuest = GetQuest(FactionType.PublicAuthority, Datamanager.Instance.data.PublicAuthority_Step);
        NowRevolutionaryArmyQuest = GetQuest(FactionType.RevolutionaryArmy, Datamanager.Instance.data.RevolutionaryArmy_Step);
        NowCultQuest = GetQuest(FactionType.Cult, Datamanager.Instance.data.Cult_Step);
        NowCrimeSyndicateQuest = GetQuest(FactionType.CrimeSyndicate, Datamanager.Instance.data.CrimeSyndicate_Step);

        // ����Ʈ ���� ��� (����׿�)
        //Debug.Log(NowPublicAuthorityQuest.questTitle);  // ����Ʈ ����
        //Debug.Log(NowRevolutionaryArmyQuest.countBlock_WeaponStore); // ����Ʈ ����
        //Debug.Log(NowRevolutionaryArmyQuest.statusCount_sentiment);


        //���º� ����Ʈ ������
        UpdatePublicText(NowPublicAuthorityQuest, PublicAuthorityText1, PublicAuthorityText2, PublicAuthorityText3);
        UpdateRevolutionaryArmyText(NowRevolutionaryArmyQuest, RevolutionaryArmyText1, RevolutionaryArmyText2, RevolutionaryArmyText3);
        UpdateCultText(NowCultQuest, CultText1, CultText2, CultText3);
        UpdateCrimeSyndicateText(NowCrimeSyndicateQuest, CrimeSyndicateText1, CrimeSyndicateText2, CrimeSyndicateText3);
    }

    private void Update()
    {
        //���Ƿ� ����Ʈ
        NowPublicAuthorityQuest.floorLimitcount = Grid3D.GridHeightLine();
        //Debug.Log((Grid3D.GridHeightLine()));
        //Debug.Log(NowPublicAuthorityQuest.floorLimitcount);

        //���� ����Ʈ
        NowRevolutionaryArmyQuest.statusCount_sentiment = (int)tetriminoStatus.GetSliderAValue();
        NowRevolutionaryArmyQuest.countBlock_WeaponStore = CountBlock.CountObjectsWithTag("WeaponStore");
        //Debug.Log(CountBlock.CountObjectsWithTag("WeaponStore"));
        //Debug.Log(NowRevolutionaryArmyQuest.statusCount_sentiment);
        //Debug.Log(NowRevolutionaryArmyQuest.statusCount_sentiment);

        //���̺� ����Ʈ
        NowCultQuest.statusCount_clear = (int)tetriminoStatus.GetSliderBValue();
        NowCultQuest.statusCount_trouble = (int)tetriminoStatus.GetSliderCValue();
        NowCultQuest.floorLimitcount = Grid3D.GridHeightLine();

        //�������� ����Ʈ
        NowCrimeSyndicateQuest.statusCount_trouble = (int)tetriminoStatus.GetSliderCValue();

        //�׽�Ʈ �뵵 �ؽ�Ʈ �� �ٲ������
        UpdatePublicText(NowPublicAuthorityQuest, PublicAuthorityText1, PublicAuthorityText2, PublicAuthorityText3);
        UpdateRevolutionaryArmyText(NowRevolutionaryArmyQuest, RevolutionaryArmyText1, RevolutionaryArmyText2, RevolutionaryArmyText3);
        UpdateCultText(NowCultQuest, CultText1, CultText2, CultText3);
        UpdateCrimeSyndicateText(NowCrimeSyndicateQuest, CrimeSyndicateText1, CrimeSyndicateText2, CrimeSyndicateText3);

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

    public void SpecialQuestObjectDestroyed(string objectTag)
    {
        if (NowPublicAuthorityQuest != null)
        {
            // ���� ���� �ı� ī��Ʈ ����
            if (objectTag == "WeaponStore")
            {
                NowPublicAuthorityQuest.countBlock_WeaponStore++;
            }
            // �ְ� ���� �ı� ī��Ʈ ���� (2�ܰ� �̻��� ���� ����)
            else if (objectTag == "CleanHouse" && NowPublicAuthorityQuest.questStep >= 2)
            {
                NowPublicAuthorityQuest.countBlock_CleanHouse++;
            }
            UpdatePublicText(NowPublicAuthorityQuest, PublicAuthorityText1, PublicAuthorityText2, PublicAuthorityText3);
        }

        if (NowCrimeSyndicateQuest != null)
        {
            // �ְŰ��� �Ǵ� ���� �ı� ī��Ʈ ����
            if (objectTag == "CleanHouse" || objectTag == "Store")
            {
                NowCrimeSyndicateQuest.countBlock_CleanHouse_Store++;
            }
            //UpdateCrimeSyndicateText(NowCrimeSyndicateQuest, CrimeSyndicateText1, CrimeSyndicateText2, CrimeSyndicateText3);
        }

    }

    public void CheckPublicAuthorityQuestComplete()
    {
        if (NowPublicAuthorityQuest == null)
            return;

        // �⺻ ������� �ı� ���� Ȯ��
        bool weaponStoreCleared = NowPublicAuthorityQuest.countBlock_WeaponStore >= NowPublicAuthorityQuest.requiredBlock_WeaponStore;

        // ���� ���� Ȯ��
        bool floorLimitCleared = NowPublicAuthorityQuest.floorLimitcount <= NowPublicAuthorityQuest.floorLimit;

        // �ְŰ��� �ı� ���� (2�ܰ� �̻��� ���� üũ)
        bool cleanHouseCleared = NowPublicAuthorityQuest.questStep == 1 ||
                                  NowPublicAuthorityQuest.countBlock_CleanHouse >= NowPublicAuthorityQuest.requiredBlock_CleanHouse;

        // ��� ���� ���� �� ����Ʈ �Ϸ� ó��
        if (weaponStoreCleared && cleanHouseCleared && floorLimitCleared)
        {
            Datamanager.Instance.data.PublicAuthority_Step++;
            Datamanager.Instance.SaveGameData();

            //Debug.Log("���Ƿ� ����Ʈ �Ϸ�! ���� �ܰ�� �̵�.");
        }
    }

    public void CheckRevolutionaryArmyQuestComplete()
    {
        if (NowRevolutionaryArmyQuest == null)
            return;

        // �ν� ���� Ȯ��
        bool sentimentCleared = NowRevolutionaryArmyQuest.statusCount_sentiment >= NowRevolutionaryArmyQuest.statusRequired_sentiment;

        // ���� ���� ���� ���� Ȯ��
        bool weaponStoreCleared = NowRevolutionaryArmyQuest.countBlock_WeaponStore >= NowRevolutionaryArmyQuest.requiredBlock_WeaponStore;

        // ��� ���� ���� �� ����Ʈ �Ϸ� ó��
        if (sentimentCleared && weaponStoreCleared)
        {
            Datamanager.Instance.data.RevolutionaryArmy_Step++;
            Datamanager.Instance.SaveGameData();

            Debug.Log("���� ����Ʈ �Ϸ�! ���� �ܰ�� �̵�.");
        }
    }

    public void CheckCultQuestComplete()
    {
        if (NowCultQuest == null)
            return;

        // û�ᵵ ���� Ȯ��
        bool cleanlinessCleared = NowCultQuest.statusCount_clear >= NowCultQuest.statusRequired_clear;

        // ���� ��ġ ���� (2�ܰ� �̻󿡼��� üũ)
        bool troubleCleared = NowCultQuest.questStep == 1 || NowCultQuest.statusCount_trouble >= NowCultQuest.statusRequired_trouble;

        // ���� ���� ���� Ȯ��
        bool floorLimitCleared = NowCultQuest.floorLimitcount >= NowCultQuest.floorLimit;

        // ��� ���� ���� �� ����Ʈ �Ϸ� ó��
        if (cleanlinessCleared && troubleCleared && floorLimitCleared)
        {
            Datamanager.Instance.data.Cult_Step++;
            Datamanager.Instance.SaveGameData();

            Debug.Log("���̺� ����Ʈ �Ϸ�! ���� �ܰ�� �̵�.");
        }
    }

    public void CheckCrimeSyndicateQuestComplete()
    {
        if (NowCrimeSyndicateQuest == null)
            return;

        // ���� ��ġ ���� Ȯ��
        bool troubleCleared = NowCrimeSyndicateQuest.statusCount_trouble >= NowCrimeSyndicateQuest.statusRequired_trouble;

        // �ְŰ��� + ���� �ı� ���� ���� Ȯ��
        bool cleanHouseStoreCleared = NowCrimeSyndicateQuest.countBlock_CleanHouse_Store >= NowCrimeSyndicateQuest.requiredBlock_CleanHouse_Store;

        // ��� ���� ���� �� ����Ʈ �Ϸ� ó��
        if (troubleCleared && cleanHouseStoreCleared)
        {
            Datamanager.Instance.data.CrimeSyndicate_Step++;
            Datamanager.Instance.SaveGameData();

            Debug.Log("�������� ����Ʈ �Ϸ�! ���� �ܰ�� �̵�.");
        }
    }

    private void UpdatePublicText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1}";
                PublicCountWeaponText.text = $"{quest.countBlock_WeaponStore}/{quest.requiredBlock_WeaponStore}";
                PublicCountCleanText.text = "";
                PublicCleanImage.SetActive(false);
            }
            else
            {
                questOrderText1.text = $"{quest.questDescription1}";
                PublicCountWeaponText.text = $"{quest.countBlock_WeaponStore}/{quest.requiredBlock_WeaponStore}";
                PublicCountCleanText.text = $"{quest.countBlock_CleanHouse}/{quest.requiredBlock_CleanHouse}";
                PublicCleanImage.SetActive(true);
            }

            questOrderText2.text = $"{quest.questDescription2}";
            PublicFloorText.text = $"{quest.floorLimitcount}/{quest.floorLimit}";
        }
    }

    private void UpdateRevolutionaryArmyText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            questOrderText1.text = $"{quest.questDescription1}";
            RevolutionsentimentyText.text = $"{quest.statusCount_sentiment}/{quest.statusRequired_sentiment}";

            questOrderText2.text = $"{quest.questDescription2}";
            RevolutionWeaponText.text = $"{quest.countBlock_WeaponStore}��";
        }
    }

    private void UpdateCultText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1}";
                CultCleanText.text = $"{quest.statusCount_clear}/{quest.statusRequired_clear}";
                CultTroubleText.text = $"";
                CultTroubleImage.SetActive(false);
            }
            else
            {
                questOrderText1.text = $"{quest.questDescription1}";
                CultCleanText.text = $"{quest.statusCount_clear}/{quest.statusRequired_clear}";
                CultTroubleText.text = $"{quest.statusCount_trouble}/{quest.statusRequired_trouble}";
                CultTroubleImage.SetActive(true);
            }

            questOrderText2.text = $"{quest.questDescription2}";
            CultFloorText.text = $"{quest.floorLimitcount}/{quest.floorLimit}";
        }
    }

    private void UpdateCrimeSyndicateText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            questOrderText1.text = $"{quest.questDescription1}";
            CrimeTroubleText.text = $"{quest.statusCount_trouble}/{quest.statusRequired_trouble}";
            
            questOrderText2.text = $"{quest.questDescription2}";
            CrimeCountText.text = $"{quest.countBlock_CleanHouse_Store}/{quest.requiredBlock_CleanHouse_Store}";
        }
    }
}