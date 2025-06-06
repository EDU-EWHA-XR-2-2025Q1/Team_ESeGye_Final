using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScanScceneIntro : MonoBehaviour
{
    [SerializeField] private GameObject speechBubblePrefab;
    [SerializeField] private Transform speechBubbleAnchor;

    private const string FirstEnterScanSceneKey = "FirstEnter_ScanScene";

    private bool hasPlayed = false;

    void Start()
    {
        // 최초 1회만 실행
        if (!PlayerPrefs.HasKey(FirstEnterScanSceneKey))
        {
            StartCoroutine(ShowScanInstructionBubble());
            PlayerPrefs.SetInt(FirstEnterScanSceneKey, 1);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        // 개발자용: R 키로 초기화해서 말풍선 다시 보기
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("말풍선 재시작용 초기화 실행");
            PlayerPrefs.DeleteKey(FirstEnterScanSceneKey);
            PlayerPrefs.Save();

            if (!hasPlayed)
            {
                StartCoroutine(ShowScanInstructionBubble());
                hasPlayed = true;
            }
        }
    }

    private IEnumerator ShowScanInstructionBubble()
    {
        string[] lines = new string[]
        {

        "어떤 동아리가 궁금해?",
        "궁금한 동아리가 있다면 \n포스터의 슬로건을 카메라로 스캔해봐!",
        "내가 바로 어떤 동아리인지 알려줄게~"
        };

        GameObject bubble = Instantiate(speechBubblePrefab, speechBubbleAnchor.position, Quaternion.identity, speechBubbleAnchor);
        TMP_Text text = bubble.GetComponentInChildren<TMP_Text>(true);

        if (text == null)
        {
            Debug.LogWarning("TMP_Text를 말풍선에서 찾지 못했습니다.");
            yield break;
        }

        bubble.SetActive(true);

        foreach (var line in lines)
        {
            yield return StartCoroutine(TypeText(text, line));
            yield return new WaitForSeconds(1.5f);
        }

        Destroy(bubble);
    }

    private IEnumerator TypeText(TMP_Text text, string line, float charDelay = 0.05f)
    {
        text.text = "";
        foreach (char c in line)
        {
            text.text += c;
            yield return new WaitForSeconds(charDelay);
        }
    }
}