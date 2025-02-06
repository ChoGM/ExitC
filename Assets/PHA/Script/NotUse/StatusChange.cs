using UnityEngine;

public class StatusChange
{
    public int statusChangeA;
    public int statusChangeB;
    public int statusChangeC;

    public StatusChange(int a, int b, int c)
    {
        statusChangeA = a;
        statusChangeB = b;
        statusChangeC = c;
    }

    // Inverse �޼��� �߰�: ������ �ݴ�� �ٲߴϴ�.
    public StatusChange Inverse()
    {
        return new StatusChange(-statusChangeA, -statusChangeB, -statusChangeC);
    }
}