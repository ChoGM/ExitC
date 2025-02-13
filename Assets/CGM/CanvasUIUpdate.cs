using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUIUpdate : MonoBehaviour
{
    void Update()
    {
        // ĵ���� ������Ʈ ���� ����
        Canvas.ForceUpdateCanvases();

        // ���̾ƿ� �׷� ���� ������Ʈ
        foreach (var layout in GetComponentsInChildren<LayoutGroup>())
        {
            layout.enabled = false;
            layout.enabled = true;
        }
    }

}
