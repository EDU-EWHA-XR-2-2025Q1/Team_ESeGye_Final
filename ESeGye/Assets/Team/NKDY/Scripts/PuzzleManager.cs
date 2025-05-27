using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가


public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzlePieces; // 퍼즐 조각 배열
    public GameObject rewardImage;    // 퍼즐 완성 시 보상 이미지
    public TMP_Text missionProgressText;  // TextMeshPro로 변경

    private int collectedCount = 0;
    private bool[] pieceCollected;

    private void Start()
    {
        pieceCollected = new bool[puzzlePieces.Length];

        // 처음에 퍼즐 조각 숨기기
        foreach (var piece in puzzlePieces)
        {
            piece.SetActive(false);
        }

        // 보상 이미지도 숨기기
        if (rewardImage != null)
            rewardImage.SetActive(false);

        UpdateMissionProgress();
    }

    // 퍼즐 조각 획득 메서드
    public void CollectPiece(int index)
    {
        if (!pieceCollected[index])
        {
            pieceCollected[index] = true;
            puzzlePieces[index].SetActive(true);
            collectedCount++;

            UpdateMissionProgress();
            CheckCompletion();
        }
    }

    // 퍼즐 완성 여부 확인
    private void CheckCompletion()
    {
        if (collectedCount == puzzlePieces.Length)
        {
            if (rewardImage != null)
                rewardImage.SetActive(true);

            Debug.Log("퍼즐 완성"); if (missionProgressText != null)
            {
                missionProgressText.text = "Mission Completed!";
            }
        }
    }

    private void UpdateMissionProgress()
    {
        if (missionProgressText != null)
        {
            missionProgressText.text = $"Mission {collectedCount}/{puzzlePieces.Length} Completed";
        }
    }
}
