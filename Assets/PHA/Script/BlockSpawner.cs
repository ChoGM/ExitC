using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject TetriseBlock1;
    public GameObject TetriseBlock2;
    public GameObject TetriseBlock3;
    public GameObject TetriseBlock4;
    public GameObject TetriseBlock5;
    public GameObject TetriseBlock6;
    public GameObject TetriseBlock7;
    public GameObject TetriseBlock8;

    private GameObject nextBlock;

    void Start()
    {
        nextBlock = SpawnNextBlock(); // ù ����� �̸� ����
    }

    // ���� ������ ��� ������ ��Ͽ��� �������� ����� �����Ͽ� ����
    public GameObject SpawnBlock()
    {
        GameObject currentBlock = nextBlock;
        nextBlock = SpawnNextBlock(); // ���� ����� �̸� �غ�
        return currentBlock;
    }

    // ������ ���� ����� �����Ͽ� ����
    private GameObject SpawnNextBlock()
    {
        int blockIndex = Random.Range(1, 9); // 1���� 8���� ���� ��
        GameObject blockPrefab = null;

        switch (blockIndex)
        {
            case 1:
                blockPrefab = TetriseBlock1;
                break;
            case 2:
                blockPrefab = TetriseBlock2;
                break;
            case 3:
                blockPrefab = TetriseBlock3;
                break;
            case 4:
                blockPrefab = TetriseBlock4;
                break;
            case 5:
                blockPrefab = TetriseBlock5;
                break;
            case 6:
                blockPrefab = TetriseBlock6;
                break;
            case 7:
                blockPrefab = TetriseBlock7;
                break;
            case 8:
                blockPrefab = TetriseBlock8;
                break;
        }

        GameObject newBlock = Instantiate(blockPrefab, transform.position, Quaternion.identity);

        // ��� ������ ���� ������ ������ ������ ����
        TetrisBlock tetrisBlock = newBlock.GetComponent<TetrisBlock>();
        if (tetrisBlock != null)
        {
            char randomValue = (char)('0' + Random.Range(1, 7)); // '1'���� '6' ������ ������ ����
            for (int x = 0; x < tetrisBlock.Width; x++)
            {
                for (int y = 0; y < tetrisBlock.Height; y++)
                {
                    for (int z = 0; z < tetrisBlock.Depth; z++)
                    {
                        if (tetrisBlock.GetBlockData()[x, y, z] != '0') // '0'�� �ƴ� ��츸 ����
                        {
                            tetrisBlock.SetBlockData(x, y, z, randomValue);
                        }
                    }
                }
            }
        }

        return newBlock;
    }

    // NextBlock�� ��ȯ�ϴ� �޼���
    public GameObject GetNextBlock()
    {
        return nextBlock;
    }
}
