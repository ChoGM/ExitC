
using UnityEngine;

public class Grid3D : MonoBehaviour
{
    public static int width = 10;   // �׸����� �ʺ� (x��)
    public static int height = 20;  // �׸����� ���� (y��)
    public static int depth = 10;   // �׸����� ���� (z��)

    public static Transform[,,] grid = new Transform[width, height, depth];

    // ���͸� �׸����� ��ǥ�� ��ȯ (�ݿø�)
    public static Vector3 Round(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }

    // �׸��� ���ο� �ִ��� Ȯ��
    public static bool InsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width &&
                (int)pos.y >= 0 && (int)pos.y < height &&
                (int)pos.z >= 0 && (int)pos.z < depth);
    }

    // Ư�� �׸��� ��ġ�� ����� �ִ��� Ȯ��
    public static Transform GetTransformAtGridPosition(Vector3 pos)
    {
        if (pos.y >= height) // �׸��带 �Ѿ�� ��ġ���� �ƹ��͵� ����
            return null;

        return grid[(int)pos.x, (int)pos.y, (int)pos.z];
    }

    // �׸��忡 ��� �߰�
    public static void AddBlockToGrid(Transform block)
    {
        foreach (Transform child in block)
        {
            Vector3 pos = Round(child.position);
            grid[(int)pos.x, (int)pos.y, (int)pos.z] = child;
        }
    }

    // ������ ���� á���� Ȯ��
    public static bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (grid[x, y, z] == null)
                    return false;
            }
        }
        return true;
    }

    // ���� ����
    public static void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                Destroy(grid[x, y, z].gameObject);
                grid[x, y, z] = null;
            }
        }
    }

    // ��ϵ��� �Ʒ��� �̵�
    public static void MoveAllBlocksDown(int y)
    {
        for (int i = y; i < height - 1; i++)
        {
            MoveLineDown(i);
        }
    }

    // Ư�� ���� �� ĭ �Ʒ��� �̵�
    public static void MoveLineDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (grid[x, y, z] != null)
                {
                    // ���� ����� �Ʒ��� �̵�
                    grid[x, y - 1, z] = grid[x, y, z];
                    grid[x, y, z] = null;
                    grid[x, y - 1, z].position += Vector3.down;
                }
            }
        }
    }

    // ���� �� ������ ã�� ����
    public static void DeleteFullLines()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                DeleteLine(y);
                MoveAllBlocksDown(y);
                y--; // ���� ���� �� �ٽ� üũ
            }
        }
    }
}
