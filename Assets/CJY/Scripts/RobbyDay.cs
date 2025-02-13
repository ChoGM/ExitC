using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyDay : MonoBehaviour
{
    public Image DayImage; // UI���� ǥ���� �̹���
    public List<Sprite> daySprites; // Day1, Day2, Day3�� �ش��ϴ� ��������Ʈ ����Ʈ

    void Start()
    {
        //Datamanager.Instance.LoadGameData();
        UpdateDayImage(Datamanager.Instance.data.NowDay);
    }

    void UpdateDayImage(int day)
    {
        if (day >= 1 && day <= daySprites.Count) // ��ȿ�� ���� Ȯ��
        {
            DayImage.sprite = daySprites[day - 1]; // NowDay ���� �´� ��������Ʈ ����
        }
    }
}
