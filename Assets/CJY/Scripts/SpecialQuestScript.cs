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
    public int requiredBlockCount_WeaponStore;
    public int requiredBlockCount_CleanHouse;
    public int requiredBlockCount_Store;

    // �ʿ� �����̴��� (����, ���̺�, ��������)
    public int statusRequired_sentiment;
    public int statusRequired_clear;
    public int statusRequired_trouble;

    public int floorLimit;             // ���� ���� (���Ƿ�, ���̺�)

    public FactionType factionType;    // ���� Ÿ�� (���Ƿ�, ����, ���̺�, ��������)
    public int questStep;              // ����Ʈ �ܰ� (1, 2, 3)
}

