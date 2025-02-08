using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactionType
{
    PublicAuthority,
    RevolutionaryArmy,
    Cult,
    CrimeSyndicate
}

[CreateAssetMenu(fileName = "NewSpecialQuest", menuName = "Quest/SpecialQuest")]
public class SpecialQuest : ScriptableObject
{
    public string questTitle;          // ����Ʈ ����
    public string questDescription1;    // ����Ʈ ����
    public string questDescription2;    // ����Ʈ ����


    // �ı��ؾ� �� ��� �� (���Ƿ�, ����, ��������)
    public int requiredBlock_WeaponStore;
    public int countBlock_WeaponStore;

    public int requiredBlock_CleanHouse;
    public int countBlock_CleanHouse;

    public int requiredBlock_Store;
    public int countBlock_Store;

    public int requiredBlock_CleanHouse_Store;
    public int countBlock_CleanHouse_Store;

    // �ʿ� �����̴��� (����, ���̺�, ��������)
    public int statusRequired_sentiment;
    public int statusCount_sentiment;

    public int statusRequired_clear;
    public int statusCount_clear;

    public int statusRequired_trouble;
    public int statusCount_trouble;

    public int floorLimit;             // ���� ���� (���Ƿ�, ���̺�)
    public int floorLimitcount;

    public FactionType factionType;    // ���� Ÿ�� (���Ƿ�, ����, ���̺�, ��������)
    public int questStep;              // ����Ʈ �ܰ� (1, 2, 3)

    public void ResetQuest()
    {
        countBlock_WeaponStore = 0;
        countBlock_CleanHouse = 0;
        countBlock_Store = 0;
        countBlock_CleanHouse_Store = 0;
        statusCount_sentiment = 0;
        statusCount_clear = 0;
        statusCount_trouble = 0;
        floorLimitcount = 0;
    }
}

