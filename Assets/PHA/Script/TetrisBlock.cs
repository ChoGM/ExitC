// Assets/Scripts/TetrisBlock.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public int Width = 4;   // ����� ���� ũ��
    public int Height = 4;  // ����� ����
    public int Depth = 4;   // ����� ����

    [SerializeField]
    private char[] blockData;  // �ν����Ϳ��� ������ �� �ִ� 1D �迭
    private char[,,] blockData3D;  // ���ο��� ����� 3D �迭

    void Start()
    {
        InitializeBlockData();
    }

    void InitializeBlockData()
    {
        if (blockData == null || blockData.Length != Width * Height * Depth)
        {
            // blockData�� null�̰ų� ũ�Ⱑ ���� ������ �⺻������ �ʱ�ȭ
            Debug.LogWarning("Invalid or missing block data, initializing with default values.");
            blockData = new char[Width * Height * Depth];
            for (int i = 0; i < blockData.Length; i++)
            {
                blockData[i] = '0'; // �⺻������ 0 ä���
            }
        }

        blockData3D = new char[Width, Height, Depth];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int z = 0; z < Depth; z++)
                {
                    int index = x + Width * (y + Height * z);
                    blockData3D[x, y, z] = blockData[index];
                }
            }
        }
    }

    public char[,,] GetBlockData()
    {
        return blockData3D;
    }

    public void SetBlockData(int x, int y, int z, char value)
    {
        if (blockData3D != null && x >= 0 && x < Width && y >= 0 && y < Height && z >= 0 && z < Depth)
        {
            blockData3D[x, y, z] = value;
        }
        else
        {
            Debug.LogError("Invalid block data position or uninitialized block data.");
        }
    }

    public void SetBlockData(char[,,] data)
    {
        if (data.GetLength(0) == Width && data.GetLength(1) == Height && data.GetLength(2) == Depth)
        {
            blockData3D = data;
        }
        else
        {
            Debug.LogError("Invalid block data size.");
        }
    }
}
