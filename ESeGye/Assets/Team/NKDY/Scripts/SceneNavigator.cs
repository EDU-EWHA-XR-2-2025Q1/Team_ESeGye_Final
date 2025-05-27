using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void GoToScanScene()
    {
        SceneManager.LoadScene("ScanScene"); // ���ΰ� ��ĵ �� �̸�
    }

    public void GoToPuzzleScene()
    {
        SceneManager.LoadScene("PuzzleScene"); // ���� ȭ�� �� �̸�
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene"); // MainScene �̸����� ����
    }

}
