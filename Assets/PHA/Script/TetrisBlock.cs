using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public int Width;   // ����� ���� ũ��
    public int Height;  // ����� ����
    public int Depth;   // ����� ����

    [SerializeField]
    private char[,,] blockData;  // ����� 3D ������
    public bool hasInteracted = false; // ��ȣ�ۿ� ���θ� ��Ÿ���� �÷���

    void Start()
    {
        // BlockData �迭�� �ν����Ϳ��� �ʱ�ȭ�� ��� ���̸� ����
        if (blockData == null || blockData.GetLength(0) != Width || blockData.GetLength(1) != Height || blockData.GetLength(2) != Depth)
        {
            blockData = new char[Width, Height, Depth];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int z = 0; z < Depth; z++)
                    {
                        blockData[x, y, z] = '0'; // �⺻�� ����
                    }
                }
            }
        }
    }

    // BlockData�� ��ȯ
    public char[,,] GetBlockData()
    {
        return blockData;
    }

    // Ư�� ��ġ�� BlockData ����
    public void SetBlockData(int x, int y, int z, char value)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height && z >= 0 && z < Depth)
        {
            blockData[x, y, z] = value;
        }
        else
        {
            Debug.LogError("Invalid block data position.");
        }
    }

    // ��ü BlockData ����
    public void SetBlockData(char[,,] data)
    {
        if (data.GetLength(0) == Width && data.GetLength(1) == Height && data.GetLength(2) == Depth)
        {
            blockData = data;
        }
        else
        {
            Debug.LogError("Invalid block data size.");
        }
    }
}
