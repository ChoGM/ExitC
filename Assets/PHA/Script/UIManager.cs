using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // �� �����̴��� ����� UI ���
    public Slider sliderA;
    public Slider sliderB;
    public Slider sliderC;

    private StatusChange statusChange;

    void Start()
    {
        // �ʱ� StatusChange �� ����
        statusChange = new StatusChange(0, 0, 0);
        UpdateSliders();
    }

    // �����̴��� ������Ʈ�ϴ� �޼���
    public void UpdateSliders()
    {
        sliderA.value = statusChange.statusChangeA;
        sliderB.value = statusChange.statusChangeB;
        sliderC.value = statusChange.statusChangeC;
    }

    // ���� ���� �� �����̴� ������Ʈ
    public void ApplyStatusChange(StatusChange change)
    {
        statusChange = change;
        UpdateSliders();
    }
}