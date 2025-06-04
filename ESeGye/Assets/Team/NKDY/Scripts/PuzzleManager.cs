using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzlePieces; // 퍼즐 조각들
    public GameObject rewardImage;    // 퍼즐 전부 획득 시 보여줄 보상 이미지
    public TMP_Text missionProgressText;

    private int collectedCount = 0;

    private void Start()
    {
        collectedCount = 0;

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            bool isCollected = PlayerPrefs.GetInt($"Mission_{i}_Complete", 0) == 1;
            puzzlePieces[i].SetActive(isCollected);

            if (isCollected)
                collectedCount++;
        }

        if (rewardImage != null)
            rewardImage.SetActive(collectedCount == puzzlePieces.Length);

        UpdateMissionProgress();
    }

    private void UpdateMissionProgress()
    {
        if (missionProgressText != null)
        {
            if (collectedCount == puzzlePieces.Length)
                missionProgressText.text = "Mission Completed!";
            else
                missionProgressText.text = $"Mission {collectedCount}/{puzzlePieces.Length} Completed";
        }
    }
}
