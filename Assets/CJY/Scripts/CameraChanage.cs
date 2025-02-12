using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanage : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    private bool isCamera1Active = true;

    void Start()
    {
        // �ʱ� ����: camera1 Ȱ��ȭ, camera2 ��Ȱ��ȭ
        camera1.enabled = true;
        camera2.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) // 0�� Ű �Է�
        {
            isCamera1Active = !isCamera1Active;
            camera1.enabled = isCamera1Active;
            camera2.enabled = !isCamera1Active;
        }
    }
}
