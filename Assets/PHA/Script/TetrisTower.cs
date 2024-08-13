using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisTower : MonoBehaviour
{
    public int Width = 4;
    public int Height = 20;
    public int Depth = 4;  // Z ���� ���� �߰�
    public float DropSpeed = 1.0f;
    public float MoveInterval = 1.0f;  // �̵� �� �� ���� �̵��� �Ÿ� (��� ũ��)

    private GameObject currentBlock;
    private BlockSpawner blockSpawner;

    void Start()
    {
        blockSpawner = FindObjectOfType<BlockSpawner>();
        SpawnNewBlock();
    }

    void Update()
    {
        HandleMovementInput();
        HandleRotationInput();

        if (currentBlock != null)
        {
            currentBlock.transform.position += Vector3.down * DropSpeed * Time.deltaTime;

            if (CheckCollision())
            {
                currentBlock.transform.position -= Vector3.down * DropSpeed * Time.deltaTime; // �浹 �� ��ġ ����
                currentBlock = null; // ���� ����� �����ϰ� ���� ��� �غ�
                SpawnNewBlock();
            }
        }
    }

    void HandleMovementInput()
    {
        if (currentBlock == null) return;

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector3.left * MoveInterval;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector3.right * MoveInterval;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector3.forward * MoveInterval;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector3.back * MoveInterval;
        }

        Vector3 newPosition = currentBlock.transform.position + moveDirection;

        if (IsValidMove(newPosition))
        {
            currentBlock.transform.position = newPosition;
        }
    }

    void HandleRotationInput()
    {
        if (currentBlock == null) return;

        Vector3 rotationAxis = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotationAxis = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            rotationAxis = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotationAxis = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            rotationAxis = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            rotationAxis = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            rotationAxis = Vector3.back;
        }

        if (rotationAxis != Vector3.zero)
        {
            currentBlock.transform.Rotate(rotationAxis, 90, Space.World);

            if (!IsValidMove(currentBlock.transform.position))
            {
                currentBlock.transform.Rotate(rotationAxis, -90, Space.World); // ȸ�� �ǵ�����
            }
        }
    }

    void SpawnNewBlock()
    {
        if (blockSpawner != null)
        {
            currentBlock = blockSpawner.SpawnBlock();
            currentBlock.transform.position = new Vector3(Width / 2, Height, Depth / 2); // Ÿ���� �߽ɿ��� ��ȯ
        }
    }

    bool IsValidMove(Vector3 position)
    {
        if (position.x < 0 || position.x >= Width ||
            position.z < 0 || position.z >= Depth || // Z ���� ��� �߰�
            position.y < 0)
        {
            return false;
        }

        // �� �κп� �ٸ� ��ϰ��� �浹 �˻縦 �߰��� �� �ֽ��ϴ�.

        return true;
    }

    bool CheckCollision()
    {
        if (currentBlock.transform.position.y <= 0)
        {
            return true;
        }

        // �ٸ� ��ϰ��� �浹 ���δ� ���⼭ ó���� �� �ֽ��ϴ�.

        return false;
    }
}
