using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzlePieces;       // 퍼즐 조각들
    public GameObject rewardImage;          // 모든 퍼즐 완성 시 보상 이미지
    public TMP_Text missionProgressText;
    public GameObject puzzleAcquiredPopup;  // 퍼즐 획득 팝업
    public GameObject speechBubblePrefab;
    public Transform speechBubbleAnchor;  // 말풍선이 생성될 위치
    public AudioSource puzzleGetAudio; // 🎵 퍼즐 조각 획득 효과음

    private int collectedCount = 0;

    private void Start()
    {
        collectedCount = 0;


        // 퍼즐 획득 팝업 기본값: 숨김
        if (puzzleAcquiredPopup != null)
            puzzleAcquiredPopup.SetActive(false);

        if (rewardImage != null)
            rewardImage.SetActive(false);

        bool showPopup = PlayerPrefs.GetInt("ShowPuzzlePopup", 0) == 1;

        // 퍼즐 조각 상태 반영
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            bool isCollected = PlayerPrefs.GetInt($"Mission_{i}_Complete", 0) == 1;
            puzzlePieces[i].SetActive(isCollected);

            if (isCollected)
                collectedCount++;
        }

        if (collectedCount == puzzlePieces.Length && rewardImage != null)
        {
            Debug.Log("보상 이미지 애니메이션");
            ShowPopupWithAnimation(rewardImage);
        }

        UpdateMissionProgress();

        // 퍼즐 팝업 보여주기
        if (showPopup)
        {
            Debug.Log("퍼즐 획득 팝업 보여줌");
            ShowPuzzleAcquiredPopup();

            // 다시는 안 뜨게 설정
            PlayerPrefs.SetInt("ShowPuzzlePopup", 0);
            PlayerPrefs.Save();
        }
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

    private void ShowPuzzleAcquiredPopup()
    {
        if (puzzleAcquiredPopup != null)
        {
            StartCoroutine(ScalePopupCoroutine());
        }

    }

    private IEnumerator ScalePopupCoroutine()
    {
        puzzleAcquiredPopup.SetActive(true);

        if (puzzleGetAudio != null)
        {
            puzzleGetAudio.Play();
            Debug.Log("🎵 퍼즐 획득 효과음 재생됨");
        }
        Transform popup = puzzleAcquiredPopup.transform;

        float duration = 0.6f;        // 한 번의 커졌다 작아지는 데 걸리는 시간
        float totalTime = 2f;         // 전체 애니메이션 시간
        float time = 0f;

        while (time < totalTime)
        {
            float t = Mathf.PingPong(time * (1f / duration), 1f); // 0 → 1 → 0 반복
            float scale = Mathf.Lerp(0.9f, 1.3f, t);              // 작았다 컸다 반복
            popup.localScale = new Vector3(scale, scale, scale);

            time += Time.deltaTime;
            yield return null;
        }

        popup.localScale = Vector3.one; // 최종 크기 원래대로
        yield return new WaitForSeconds(2f);
        HidePuzzlePopup();

        yield return new WaitForSeconds(0.2f); // 부드러운 전환

        // 말풍선 등장
        yield return StartCoroutine(ShowTooltip()); 
    }


    private IEnumerator ShowTooltip()
    {
        Debug.Log("ShowTooltip called");

        if (speechBubblePrefab == null || speechBubbleAnchor == null)
        {
            Debug.LogWarning("speechBubblePrefab 또는 anchor가 연결되지 않았습니다.");
            yield break;
        }

        GameObject bubbleInstance = Instantiate(speechBubblePrefab, speechBubbleAnchor.position, Quaternion.identity, speechBubbleAnchor);

        TMP_Text textComponent = bubbleInstance.GetComponentInChildren<TMP_Text>(true);
        if (textComponent == null)
        {
            Debug.LogWarning("TMP_Text를 프리팹에서 찾지 못했습니다.");
            yield break;
        }

        yield return StartCoroutine(ShowSpeechBubbleSequence(bubbleInstance, textComponent));
    }

    private IEnumerator ShowSpeechBubbleSequence(GameObject bubble, TMP_Text text)
    {
        string[] lines = new string[]
        {
        "우와~ 첫 퍼즐 조각을 하나 획득했어!",
        "총 4개를 모으면 특별한 보상이 기다리고 있어!",
        "다음 미션도 도전해봐!"
        };

        bubble.SetActive(true);
        Debug.Log("말풍선 활성화");

        for (int i = 0; i < lines.Length; i++)
        {
           // bubble.SetActive(true);
            yield return StartCoroutine(TypeText(text, lines[i]));
            yield return new WaitForSeconds(1.5f);
        }

        Debug.Log("말풍선 제거");
        Destroy(bubble);
    }

    private IEnumerator TypeText(TMP_Text text, string fullText, float charDelay = 0.05f)
    {
        text.text = "";
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(charDelay);
        }

        Debug.Log($"TypeText 완료: {fullText}");
    }

    private void HidePuzzlePopup()
    {
        if (puzzleAcquiredPopup != null)
            puzzleAcquiredPopup.SetActive(false);
    }

    private void ShowPopupWithAnimation(GameObject target)
    {
        StartCoroutine(ScaleTargetCoroutine(target));

    }

    private IEnumerator ScaleTargetCoroutine(GameObject target)
    {
        target.SetActive(true);
        Transform popup = target.transform;

        float duration = 0.6f;
        float totalTime = 2f;
        float time = 0f;

        while (time < totalTime)
        {
            float t = Mathf.PingPong(time * (1f / duration), 1f); // 0 → 1 → 0 반복
            float scale = Mathf.Lerp(0.9f, 1.3f, t);
            popup.localScale = new Vector3(scale, scale, scale);

            time += Time.deltaTime;
            yield return null;
        }

        popup.localScale = Vector3.one;
    }

}

