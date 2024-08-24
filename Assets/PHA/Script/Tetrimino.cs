using UnityEngine;

public class Tetrimino : MonoBehaviour
{
    public Vector3[] blockPositions;
    private float fallTime = 1.0f;
    private float lockDelay = 1.0f;
    private float previousTime;
    private float lastMoveTime;
    private float previousYPosition;
    private bool isLocked = false;

    void Start()
    {
        CreateBlocks();
        previousTime = Time.time;
        lastMoveTime = Time.time;
        previousYPosition = transform.position.y;
    }

    void Update()
    {
        if (isLocked) return;

        HandleInput();

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

    void HandleInput()
    {
        bool inputReceived = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);  // X�� �������� �̵�
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);  // X�� ���������� �̵�
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.forward);  // Z�� ������ �̵�
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.back);  // Z�� �ڷ� �̵�
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector3.down);  // Y�� �Ʒ��� �̵�
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(Vector3.right);  // X�� ȸ��
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate(Vector3.forward);  // Z�� ȸ��
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();  // �� �Ʒ��� �̵�
            inputReceived = true;
        }

        if (inputReceived)
        {
            lastMoveTime = Time.time;
        }
    }

    void Move(Vector3 direction)
    {
        transform.position += direction;

        if (!IsValidPosition())
        {
            transform.position -= direction;
        }
    }

    void Rotate(Vector3 axis)
    {
        transform.Rotate(axis * 90);

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
        transform.position -= Vector3.down;  // ���� ��ġ ����
        LockTetrimino();  // ��� ��� ����
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
        Grid3D.DeleteFullLines();
        FindObjectOfType<GameManager>().OnBlockLanded();
        enabled = false;
    }

    void CreateBlocks()
    {
        foreach (Vector3 pos in blockPositions)
        {
            GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.position = transform.position + pos;
            block.transform.parent = this.transform;
        }
    }
}
