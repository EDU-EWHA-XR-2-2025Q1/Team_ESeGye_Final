using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    public void OnClickBackToScan()
    {
        SceneManager.LoadScene("ScanScene");  // "ScanScene" 이름은 실제 씬 이름으로 바꿔주세요
    }
}
