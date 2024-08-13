using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] TetrisBlocks; // �پ��� ��Ʈ���� ��� �������� ���⿡ ����

    public GameObject SpawnBlock()
    {
        int blockIndex = Random.Range(0, TetrisBlocks.Length);
        return Instantiate(TetrisBlocks[blockIndex], transform.position, Quaternion.identity);
    }
}
