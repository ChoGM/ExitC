using UnityEngine;
using UnityEngine.EventSystems;

public class HoverCanvasActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Canvas targetCanvas; // Ȱ��ȭ�� Canvas

    void Start()
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false); // ó������ ��Ȱ��ȭ
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true); // ���콺 ȣ�� �� Ȱ��ȭ
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false); // ���콺 ����� ��Ȱ��ȭ
        }
    }
}
