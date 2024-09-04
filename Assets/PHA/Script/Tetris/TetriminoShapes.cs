using UnityEngine;

public class TetriminoShapes : MonoBehaviour
{
    public UIManager uiManager;

    void Start()
    {
        // ���� StatusChange ���� �� UI ������Ʈ
        StatusChange randomChange = GetRandomStatusChange();
        uiManager.ApplyStatusChange(randomChange);
    }

    // �� ��Ʈ���̳��� ����� ����
    public static Vector3[] IShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(3, 0, 0)
    };

    public static Vector3[] OShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)
    };

    public static Vector3[] TShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0)
    };

    public static Vector3[] LShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(0, 1, 0)
    };

    public static Vector3[] JShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(-2, 0, 0), new Vector3(0, 1, 0)
    };

    public static Vector3[] SShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(-1, 1, 0)
    };

    public static Vector3[] ZShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)
    };

    public static StatusChange GetRandomStatusChange()
    {
        int randomStatus = Random.Range(0, 3); // 0: A, 1: B, 2: C
        int changeValue = Random.Range(0, 2) == 0 ? -1 : 1; // -1 �Ǵ� +1

        switch (randomStatus)
        {
            case 0:
                return new StatusChange(changeValue, 0, 0); // A�� ����
            case 1:
                return new StatusChange(0, changeValue, 0); // B�� ����
            case 2:
                return new StatusChange(0, 0, changeValue); // C�� ����
            default:
                return new StatusChange(0, 0, 0); // �� �⺻���� ���� ���õ��� ����
        }
    }
}