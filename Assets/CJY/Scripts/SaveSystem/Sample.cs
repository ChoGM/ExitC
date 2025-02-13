using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        Datamanager.Instance.LoadGameData();

        Debug.Log("���� ������ ���� ���: " + Application.persistentDataPath);

    }

    void Update()
    {
    }

    //������ �����ϸ� �ڵ�����
    private void OnApplicationQuit()
    {
        Datamanager.Instance.SaveGameData();
    }


    public void ClearDayGame()
    {
        if (Datamanager.Instance.data.NowDay < 3)
            Datamanager.Instance.data.NowDay += 1;
        else
            Debug.Log("���� ���̻� Ŀ�� �� ����");

        Datamanager.Instance.SaveGameData();
    }

    public void BackDayGame()
    {
        if (Datamanager.Instance.data.NowDay >1)
            Datamanager.Instance.data.NowDay -= 1;
        else
            Debug.Log("���� ���̻� �۾��� �� ����");

        Datamanager.Instance.SaveGameData();
    }
}
