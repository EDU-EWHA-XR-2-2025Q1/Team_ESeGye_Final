using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�


public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzlePieces; // ���� ���� �迭
    public GameObject rewardImage;    // ���� �ϼ� �� ���� �̹���
    public TMP_Text missionProgressText;  // TextMeshPro�� ����

    private int collectedCount = 0;
    private bool[] pieceCollected;

    private void Start()
    {
        pieceCollected = new bool[puzzlePieces.Length];

        // ó���� ���� ���� �����
        foreach (var piece in puzzlePieces)
        {
            piece.SetActive(false);
        }

        // ���� �̹����� �����
        if (rewardImage != null)
            rewardImage.SetActive(false);

        UpdateMissionProgress();
    }

    // ���� ���� ȹ�� �޼���
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

    // ���� �ϼ� ���� Ȯ��
    private void CheckCompletion()
    {
        if (collectedCount == puzzlePieces.Length)
        {
            if (rewardImage != null)
                rewardImage.SetActive(true);

            Debug.Log("���� �ϼ�"); if (missionProgressText != null)
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
