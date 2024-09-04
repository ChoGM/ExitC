using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject tetriminoPrefab;

    // UI �����̴��� ���� �����̴����� �����ֱ� ���� �ʵ�
    public Slider statusASlider;
    public Slider statusBSlider;
    public Slider statusCSlider;

    // �����̴��� �� ����
    public int statusA;
    public int statusB;
    public int statusC;

    void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ���� ���� �� ù ��Ʈ���̳� ��ȯ
        SpawnTetrimino();
    }

    public void SpawnTetrimino()
    {
        Vector3 spawnPosition = new Vector3(2, 10, 2);
        GameObject tetrimino = Instantiate(tetriminoPrefab, spawnPosition, Quaternion.identity);
        Tetrimino tetriminoComponent = tetrimino.GetComponent<Tetrimino>();

        // �� ����� �������� �����ϰ�, �����̴��� ��ȭ�� �������� ����
        Vector3[] shape = GetRandomShape(out StatusChange statusChange);
        tetriminoComponent.blockPositions = shape;
        tetriminoComponent.statusChange = statusChange;

        // ���� ���͸��� ����
        tetriminoComponent.ApplyMaterial();
    }

    Vector3[] GetRandomShape(out StatusChange statusChange)
    {
        // �������� ��� ����
        int shapeIndex = Random.Range(0, 7);
        switch (shapeIndex)
        {
            case 0: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.IShape;
            case 1: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.OShape;
            case 2: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.TShape;
            case 3: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.LShape;
            case 4: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.JShape;
            case 5: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.SShape;
            case 6: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.ZShape;
            default: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.IShape;
        }
    }

    public void UpdateStatus(StatusChange statusChange)
    {
        statusA += statusChange.statusChangeA;
        statusB += statusChange.statusChangeB;
        statusC += statusChange.statusChangeC;

        // �����̴����� ����Ǿ��� �� UI ������Ʈ
        UpdateStatusUI();
    }

    void UpdateStatusUI()
    {
        if (statusASlider != null)
        {
            statusASlider.value = statusA;
        }
        if (statusBSlider != null)
        {
            statusBSlider.value = statusB;
        }
        if (statusCSlider != null)
        {
            statusCSlider.value = statusC;
        }
    }

    public void OnBlockLanded()
    {
        // ���� �������� �� ȣ��Ǿ� ���ο� ���� ��ȯ�մϴ�.
        SpawnTetrimino();
    }
}
