using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject tetriminoPrefab;
    public GameObject nextTetriminoPrefab;  // NextBlock �̸����⸦ ���� ������
    public Transform nextBlockDisplayPosition;  // NextBlock UI ��ġ

    public Slider statusASlider;
    public Slider statusBSlider;
    public Slider statusCSlider;

    //public Slider status1Slider;
    //public Slider status2Slider;
    //public Slider status3Slider;

    public int statusA;
    public int statusB;
    public int statusC;

    public GameObject gameOverUI;

    private bool isGameOver = false;
    private Tetrimino currentTetrimino;  // ���� ���
    private Tetrimino nextTetrimino;     // ���� ���

    void Awake()
    {
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
        // NextBlock �ʱ�ȭ �� ù ��� ����
        nextTetrimino = SpawnNextTetrimino();
        SpawnTetrimino();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwapWithNextBlock();  // CŰ �Է� �� ��� ��ü
        }

        //// TriggerGameOver ȣ�� ����
        //if (status1Slider.value <= 45 || status2Slider.value <= 45 || status3Slider.value <= 45)
        //{
        //    TriggerGameOver();
        //}
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }

    public void SpawnTetrimino()
    {
        if (isGameOver) return;

        Vector3 spawnPosition = new Vector3(2, 18, 2);

        if (IsPositionOccupied(spawnPosition, nextTetrimino))
        {
            TriggerGameOver();
            return;
        }

        currentTetrimino = nextTetrimino;
        currentTetrimino.transform.position = spawnPosition;
        currentTetrimino.enabled = true;

        // ���� ��ȯ�� ���� ���� ���ο� �׸��� ����
        currentTetrimino.CreateShadow();
        currentTetrimino.UpdateShadowPosition();

        nextTetrimino = SpawnNextTetrimino();
    }


    private bool IsPositionOccupied(Vector3 position, Tetrimino tetrimino)
    {
        // tetrimino�� null�� �ƴ� ��쿡�� �ڽ� ��ġ�� Ȯ��
        if (tetrimino == null) return false;

        foreach (Transform child in tetrimino.transform)
        {
            Vector3 pos = Grid3D.Round(position + child.localPosition);
            if (!Grid3D.InsideGrid(pos) || Grid3D.GetTransformAtGridPosition(pos) != null)
            {
                return true;
            }
        }
        return false;
    }


    public Tetrimino SpawnNextTetrimino()
    {
        // NextBlock ��ġ�� ����
        Vector3 spawnPosition = nextBlockDisplayPosition.position;
        GameObject nextTetriminoObject = Instantiate(tetriminoPrefab, spawnPosition, Quaternion.identity);
        Tetrimino tetriminoComponent = nextTetriminoObject.GetComponent<Tetrimino>();

        // �� ����� �������� ����
        Vector3[] shape = GetRandomShape(out StatusChange statusChange);
        tetriminoComponent.blockPositions = shape;
        tetriminoComponent.statusChange = statusChange;

        // ����� �����ϴ� CreateBlocks() ȣ���Ͽ� ����� �׸���
        tetriminoComponent.CreateBlocks();

        tetriminoComponent.ApplyMaterial();  // �߰��� �κ�: ���͸����� ����

        // NextBlock�� �������� �ʵ��� ��Ȱ��ȭ
        tetriminoComponent.enabled = false;

        return tetriminoComponent;
    }
    // GameManager.cs


    public void SwapWithNextBlock()
    {
        if (currentTetrimino == null || nextTetrimino == null) return;

        currentTetrimino.DestroyShadow(); // ���� ���� �׸��� ��Ȱ��ȭ

        Vector3 currentPosition = currentTetrimino.transform.position;
        Vector3 nextBlockPosition = nextBlockDisplayPosition.position;

        currentTetrimino.transform.position = nextBlockPosition;
        nextTetrimino.transform.position = currentPosition;

        Tetrimino temp = currentTetrimino;
        currentTetrimino = nextTetrimino;
        nextTetrimino = temp;

        // ���ο� currentTetrimino�� ���� �׸��� �����/Ȱ��ȭ
        if (currentTetrimino.shadowTetrimino == null)
        {
            currentTetrimino.CreateShadow();
        }
        else
        {
            currentTetrimino.shadowTetrimino.SetActive(true);
        }

        currentTetrimino.UpdateShadowPosition();
        currentTetrimino.enabled = true;
        nextTetrimino.enabled = false;
    }


    Vector3[] GetRandomShape(out StatusChange statusChange)
    {
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

    public void OnBlockLanded()
    {
        SpawnTetrimino();
    }


    public void UpdateStatus(StatusChange statusChange)
    {
        // �������ͽ� ���� ����
        statusA += statusChange.statusChangeA;
        statusB += statusChange.statusChangeB;
        statusC += statusChange.statusChangeC;

        // UI �����̴� ������Ʈ
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
}
