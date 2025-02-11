using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void Select()
    {
        SceneManager.LoadScene("Select");
    }

    public void CJYScene()
    {
        SceneManager.LoadScene("CJYScene");
    }

    public void MainTittleScene()
    {
        SceneManager.LoadScene("MainTittleScene");
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // ������ ����
#else
    Application.Quit();  // ����� ���� ����
#endif
    }
}
