using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ʈ���� �� 3D������ �޾ƾ� �� hyeon gpt���� �̾ �ϱ�

public class TetrisTower : MonoBehaviour
{
    public int Width = 4;
    public int Height = 4;
    public int Depth = 10;
    public float interval = 10.0f;
    public float dropSpeed = 1.0f;

    private GameObject currentBlock;
    private BlockSpawner blockSpawner;

    private List<GameObject> activeBlocks = new List<GameObject>();
    private char[] Blocks;
    private bool blockDataLoaded = false;

    void Start()
    {
        Blocks = new char[Width * Height * Depth];
        blockSpawner = FindObjectOfType<BlockSpawner>();
        currentBlock = blockSpawner.SpawnBlock();
        StartCoroutine(DropBlock(currentBlock));
    }

    IEnumerator DropBlock(GameObject block)
    {
        while (true)
        {
            block.transform.position += Vector3.down * dropSpeed * Time.deltaTime;

            if (CheckCollision(block))
            {
                block.transform.position = new Vector3(
                    Mathf.Round(block.transform.position.x / interval) * interval,
                    Mathf.Round(block.transform.position.y / interval) * interval,
                    Mathf.Round(block.transform.position.z / interval) * interval
                );

                activeBlocks.Remove(block);
                CheckAndClearFullFloors();

                // ���ο� ��� ����
                currentBlock = blockSpawner.SpawnBlock();
                StartCoroutine(DropBlock(currentBlock));
                break;
            }

            HandleMovementInput(block); // �Է� ó�� �߰�

            yield return null;
        }
    }

    void HandleMovementInput(GameObject block)
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector3.left * interval;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector3.right * interval;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector3.forward * interval;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector3.back * interval;
        }

        Vector3 newPosition = block.transform.position + moveDirection;

        // �̵� �� �浹 �˻縦 ���� �̵��� �������� Ȯ��
        if (IsValidMove(newPosition))
        {
            block.transform.position = newPosition;
        }
    }

    bool IsValidMove(Vector3 position)
    {
        // Ÿ���� ��踦 ���� �ʴ��� Ȯ��
        if (position.x < 0 || position.x >= Width * interval ||
            position.z < 0 || position.z >= Depth * interval)
        {
            return false;
        }

        // �ٸ� ��ϰ��� �浹 ���� Ȯ��
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, interval))
        {
            if (hit.collider != null && hit.collider.gameObject != currentBlock)
            {
                return false;
            }
        }

        return true;
    }

    bool CheckCollision(GameObject block)
    {
        RaycastHit hit;
        if (Physics.Raycast(block.transform.position, Vector3.down, out hit, interval))
        {
            if (hit.collider != null && hit.collider.gameObject != block)
            {
                return true;
            }
        }

        return false;
    }

    void CheckAndClearFullFloors()
    {
        for (int y = 0; y < Height; y++)
        {
            bool isFloorFull = true;
            for (int z = 0; z < Depth; z++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = z * (Width * Height) + y * Width + x;
                    if (Blocks[index] == '0')
                    {
                        isFloorFull = false;
                        break;
                    }
                }
                if (!isFloorFull) break;
            }

            if (isFloorFull)
            {
                ClearFloor(y);
                DropBlocksAbove(y);
            }
        }
    }

    void ClearFloor(int y)
    {
        for (int z = 0; z < Depth; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                int index = z * (Width * Height) + y * Width + x;
                Blocks[index] = '0';

                RaycastHit hit;
                Vector3 position = new Vector3(x, y, z) * interval;
                if (Physics.Raycast(position, Vector3.up, out hit, interval))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    void DropBlocksAbove(int y)
    {
        for (int currentY = y + 1; currentY < Height; currentY++)
        {
            for (int z = 0; z < Depth; z++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int currentIndex = z * (Width * Height) + currentY * Width + x;
                    int belowIndex = z * (Width * Height) + (currentY - 1) * Width + x;

                    if (Blocks[currentIndex] != '0')
                    {
                        Blocks[belowIndex] = Blocks[currentIndex];
                        Blocks[currentIndex] = '0';

                        RaycastHit hit;
                        Vector3 position = new Vector3(x, currentY, z) * interval;
                        if (Physics.Raycast(position, Vector3.up, out hit, interval))
                        {
                            GameObject block = hit.collider.gameObject;
                            block.transform.position += Vector3.down * interval;
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateActiveBlocks(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            RotateActiveBlocks(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            RotateActiveBlocks(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            RotateActiveBlocks(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RotateActiveBlocks(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            RotateActiveBlocks(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) // NextBlock�� ��ü�ϴ� ���
        {
            SwapCurrentAndNextBlock();
        }
    }

    void RotateActiveBlocks(Vector3 axis)
    {
        if (currentBlock != null)
        {
            currentBlock.transform.Rotate(axis, 90f);
        }
    }

    void SwapCurrentAndNextBlock()
    {
        if (currentBlock != null && blockSpawner != null)
        {
            GameObject nextBlock = blockSpawner.GetNextBlock();
            Vector3 currentPos = currentBlock.transform.position;

            Destroy(currentBlock);
            currentBlock = Instantiate(nextBlock, currentPos, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        TetrisBlock tetrisBlock = other.gameObject.GetComponent<TetrisBlock>();

        if (tetrisBlock != null && !blockDataLoaded && !tetrisBlock.hasInteracted)
        {
            char[] blockData = tetrisBlock.GetBlockData();
            LoadTower(blockData);
            blockDataLoaded = true;
            tetrisBlock.hasInteracted = true;
        }
    }

    void LoadTower(char[] blockData)
    {
        // blockData�� ��ġ�� ���� ��Ͽ� ���� �����͸� �����ϰ� �ִٰ� ����
        for (int i = 0; i < blockData.Length; i++)
        {
            if (blockData[i] != '0') // '0'�� �� ������ ��Ÿ��
            {
                // ���� �ε��� i�κ��� x, y, z�� ���
                int x = i % Width;
                int y = (i / Width) % Height;
                int z = i / (Width * Height);

                int index = z * (Width * Height) + y * Width + x;

                // Blocks �迭 ������Ʈ
                Blocks[index] = blockData[i];
            }
        }
    }
}
