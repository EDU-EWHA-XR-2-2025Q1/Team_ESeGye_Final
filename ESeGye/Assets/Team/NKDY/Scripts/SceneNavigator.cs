using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void GoToScanScene()
    {
        SceneManager.LoadScene("ScanScene"); // 슬로건 스캔 씬 이름
    }

    public void GoToPuzzleScene()
    {
        SceneManager.LoadScene("PuzzleScene"); // 퍼즐 화면 씬 이름
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene"); // MainScene 이름으로 설정
    }

}
