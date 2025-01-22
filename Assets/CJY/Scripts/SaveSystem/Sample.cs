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

    public void ChapterUnlock(int chapterNum)
    {
        //é�� �������
        Datamanager.Instance.data.isUnlock[chapterNum] = true;

        Datamanager.Instance.SaveGameData();
    }
}
