using UnityEngine;

public class Tetrimino : MonoBehaviour
{
    public Vector3[] blockPositions;
    public StatusChange statusChange;

    // ���͸����� ���� �ʵ� �߰�
    public Material statusAIncreaseMaterial;
    public Material statusADecreaseMaterial;
    public Material statusBIncreaseMaterial;
    public Material statusBDecreaseMaterial;
    public Material statusCIncreaseMaterial;
    public Material statusCDecreaseMaterial;

    private float fallTime = 1.0f;
    private float lockDelay = 1.0f;
    private float previousTime;
    private float lastMoveTime;
    private float previousYPosition;
    private bool isLocked = false;

    // Tetrimino.cs

    public GameObject shadowTetrimino;

    void Start()
    {
        CreateBlocks();
        CreateShadow();
        UpdateShadowPosition();

        previousTime = Time.time;
        lastMoveTime = Time.time;
        previousYPosition = transform.position.y;

        // ���͸��� ����
        ApplyMaterial();
    }

    void Update()
    {
        if (isLocked) return;

        HandleInput();

        // �� �̵� �� ȸ�� �� �׸��� ��ġ ����
        UpdateShadowPosition();

        // �Ʒ������� �ڵ����� �������� ���� ó��
        if (Time.time - previousTime >= fallTime)
        {
            Move(Vector3.down);

            if (Mathf.Abs(transform.position.y - previousYPosition) > Mathf.Epsilon)
            {
                lastMoveTime = Time.time;
                previousYPosition = transform.position.y;
            }

            if (Time.time - lastMoveTime >= lockDelay)
            {
                LockTetrimino();
            }

            previousTime = Time.time;
        }
    }


    // Tetrimino.cs

    public void CreateShadow()
    {
        if (shadowTetrimino == null)
        {
            shadowTetrimino = new GameObject("ShadowTetrimino");
            foreach (Transform block in transform)
            {
                GameObject shadowBlock = Instantiate(block.gameObject, shadowTetrimino.transform);
                shadowBlock.GetComponent<Renderer>().material.color = Color.gray; // �׸��� ���� ����
                Destroy(shadowBlock.GetComponent<Collider>()); // �浹 ����
            }
        }
        shadowTetrimino.SetActive(true); // �� �� Ȱ��ȭ �� �׸��� Ȱ��ȭ
        UpdateShadowPosition();
    }

    public void DestroyShadow()
    {
        if (shadowTetrimino != null)
        {
            shadowTetrimino.SetActive(false); // �׸��ڸ� �������� �ʰ� ��Ȱ��ȭ
        }
    }

    public void UpdateShadowPosition()
    {
        if (shadowTetrimino == null) return;

        shadowTetrimino.transform.position = transform.position;
        shadowTetrimino.transform.rotation = transform.rotation;

        while (IsValidShadowPosition())
        {
            shadowTetrimino.transform.position += Vector3.down;
        }
        shadowTetrimino.transform.position -= Vector3.down;
    }

    bool IsValidShadowPosition()
    {
        foreach (Transform shadowBlock in shadowTetrimino.transform)
        {
            Vector3 pos = Grid3D.Round(shadowBlock.position);
            if (!Grid3D.InsideGrid(pos) || Grid3D.GetTransformAtGridPosition(pos) != null)
            {
                return false;
            }
        }
        return true;
    }



    void HandleInput()
    {
        bool inputReceived = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.forward);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.back);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector3.down);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(Vector3.right);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate(Vector3.forward);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();  // ���� �ٷ� �ٴ����� ����߸�
            inputReceived = true;
        }

        if (inputReceived)
        {
            lastMoveTime = Time.time;
        }
    }

    // Tetrimino.cs

    public void MoveToValidPosition()
    {
        Vector3 adjustment = Vector3.zero;

        foreach (Transform child in transform)
        {
            Vector3 pos = Grid3D.Round(child.position);

            if (pos.x < 0) adjustment.x = Mathf.Max(adjustment.x, -pos.x);
            if (pos.x >= Grid3D.width) adjustment.x = Mathf.Min(adjustment.x, Grid3D.width - 1 - pos.x);
            if (pos.y < 0) adjustment.y = Mathf.Max(adjustment.y, -pos.y);
            if (pos.y >= Grid3D.height) adjustment.y = Mathf.Min(adjustment.y, Grid3D.height - 1 - pos.y);
            if (pos.z < 0) adjustment.z = Mathf.Max(adjustment.z, -pos.z);
            if (pos.z >= Grid3D.depth) adjustment.z = Mathf.Min(adjustment.z, Grid3D.depth - 1 - pos.z);
        }

        transform.position += adjustment;
    }

    void Move(Vector3 direction)
    {
        transform.position += direction;
        MoveToValidPosition();

        if (!IsValidPosition())
        {
            transform.position -= direction;
        }
    }

    void Rotate(Vector3 axis)
    {
        transform.Rotate(axis * 90);
        MoveToValidPosition();

        if (!IsValidPosition())
        {
            transform.Rotate(-axis * 90);
        }
    }

    void HardDrop()
    {
        while (IsValidPosition())
        {
            transform.position += Vector3.down;
        }
        transform.position -= Vector3.down;
        LockTetrimino();
    }

    bool IsValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector3 pos = Grid3D.Round(child.position);
            if (!Grid3D.InsideGrid(pos) || Grid3D.GetTransformAtGridPosition(pos) != null)
            {
                return false;
            }
        }
        return true;
    }

    void LockTetrimino()
    {
        isLocked = true;
        Grid3D.AddBlockToGrid(transform);
        GameManager.Instance.UpdateStatus(statusChange);
        Grid3D.DeleteFullLines();
        FindObjectOfType<GameManager>().OnBlockLanded();
        DestroyShadow();  // ���� �����Ǹ� �׸��� ����
        enabled = false;
    }



    void OnDestroy()
    {
        if (isLocked)
        {
            GameManager.Instance.UpdateStatus(statusChange.Inverse());
        }
    }

    public void CreateBlocks()
    {
        // �� ����� ��ġ�� ť�긦 �����Ͽ� ����� ����
        foreach (Vector3 pos in blockPositions)
        {
            GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.position = transform.position + pos;
            block.transform.parent = this.transform;
        }
    }


    public void ApplyMaterial()
    {
        Material selectedMaterial = null;

        if (statusChange.statusChangeA > 0)
        {
            selectedMaterial = statusAIncreaseMaterial;
        }
        else if (statusChange.statusChangeA < 0)
        {
            selectedMaterial = statusADecreaseMaterial;
        }
        else if (statusChange.statusChangeB > 0)
        {
            selectedMaterial = statusBIncreaseMaterial;
        }
        else if (statusChange.statusChangeB < 0)
        {
            selectedMaterial = statusBDecreaseMaterial;
        }
        else if (statusChange.statusChangeC > 0)
        {
            selectedMaterial = statusCIncreaseMaterial;
        }
        else if (statusChange.statusChangeC < 0)
        {
            selectedMaterial = statusCDecreaseMaterial;
        }

        if (selectedMaterial != null)
        {
            foreach (Transform child in transform)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = selectedMaterial;
                }
            }
        }
    }
}
