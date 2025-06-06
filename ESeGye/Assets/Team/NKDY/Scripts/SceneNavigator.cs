using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void GoToScanSceneFromMain()
    {
        Debug.Log("▶ GoToScanSceneFromMain 호출됨");
        SceneManager.LoadScene("ScanScene");
    }

    public void GoToPuzzleSceneFromMain()
    {
        Debug.Log("▶ GoToPuzzleSceneFromMain 호출됨");
        SceneManager.LoadScene("PuzzleScene");
    }

    public void GoToMainSceneFromMain()
    {
        Debug.Log("▶ GoToMainSceneFromMain 호출됨");
        SceneManager.LoadScene("MainScene");
    }

    public void GoToPlaySceneFromMain()
    {
        Debug.Log("▶ GoToPlaySceneFromMain 호출됨");
        SceneManager.LoadScene("PlayScene");
    }
    
    public void GoToSettingFromMain()
    {
        Debug.Log("▶ GoToSettingFromMain 호출됨");
        SceneManager.LoadScene("Setting");
    }

    public void GoToFavoriteClubFromMain()
    {
        Debug.Log("▶ GoToFavoriteClubFromMain 호출됨");
        SceneManager.LoadScene("Favorite");
    }

    public void GoToMissionFromMain()
    {
        Debug.Log("▶ GoToMissionFromMain 호출됨");
        SceneManager.LoadScene("MissionScene");
    }
}