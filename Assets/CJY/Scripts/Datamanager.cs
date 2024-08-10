using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;


public class Datamanager : MonoBehaviour
{
    static GameObject container;

    //�̱��� 
    static Datamanager instance;
    public static Datamanager Instance
    {
        get
        {
            if (!instance)
            {
                //���ο� ������Ʈ ����
                container = new GameObject();
                //������Ʈ �̸� ����
                container.name = "Datamanager";
                //���ο� ������Ʈ�� ������ �Ŵ��� �ְ�, �ν��Ͻ��� �Ҵ�
                instance = container.AddComponent(typeof(Datamanager)) as Datamanager;
                //�ش� ������Ʈ�� ������� �ʵ��� ����
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    //���� ������ ���� �̸� ����
    string GameDataFileName = "GameData.json";

    //����� Ŭ���� ����
    public Data data = new Data();

    string filename = "save";

    // �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(FromJsonData);
            print("�ҷ����� �Ϸ�");
        }
    }


    // �����ϱ�
    public void SaveGameData()
    {
        // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
        File.WriteAllText(filePath, ToJsonData);
    }
}
