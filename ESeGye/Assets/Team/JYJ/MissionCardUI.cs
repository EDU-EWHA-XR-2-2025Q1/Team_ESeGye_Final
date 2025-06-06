using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class MissionCardUI : MonoBehaviour
{
    [Header("UI 연결")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI puzzleStatusText;
    public Image emojiImg;

    [Header("미션 이미지")]
    public List<Sprite> emojiSprites;

    [Header("학문관 미션 버튼")]
    public GameObject arriveButton;

    private int currentIndex = 0;

    // 미션 완료 상태
    private bool[] missionCompleted = new bool[4]; // 초기값 전부 false

    private const string FirstEnterMissionSceneKey = "FirstEnter_MissionScene";
    [SerializeField] private GameObject speechBubblePrefab;
    [SerializeField] private Transform speechBubbleAnchor;
    [SerializeField] private GameObject dimmedBackground;  

    private readonly string[] titles = {
        "Mission 1: 학문관 도착",
        "Mission 2: 첫 스캔 성공",
        "Mission 3: 동일 태그 동아리 3개",
        "Mission 4: 모집중 동아리 찾기"
    };

    private readonly string[] descriptions = {
        "이화여대 중앙동아리 정보를\n얻을 수 있는 학문관으로 이동하세요. ",
        "카메라를 활용해 처음으로 동아리 슬로건을 스캔해보세요.",
        "공통된 태그를 가진 3개의 동아리를 스캔해보고, 내 관심사를 확인해보세요!",
        "현재 모집 중인 동아리를 찾아 스캔하세요. 내 첫 지원은 어떤 동아리일까요?"
    };

    void Start()
    {
        if (dimmedBackground != null)
            dimmedBackground.SetActive(false);
            
        // 최초 1회만 초기화
        if (!PlayerPrefs.HasKey("MissionsInitialized"))
        {
            ResetMissionsAtStart();
            PlayerPrefs.SetInt("MissionsInitialized", 1);
            PlayerPrefs.Save();
        }

        // 최초 진입 시 말풍선
        if (!PlayerPrefs.HasKey(FirstEnterMissionSceneKey))
        {
            StartCoroutine(ShowIntroSpeechBubble());
            PlayerPrefs.SetInt(FirstEnterMissionSceneKey, 1);
            PlayerPrefs.Save();
        }

        // 저장된 퍼즐 상태 불러오기
        for (int i = 0; i < missionCompleted.Length; i++)
        {
            missionCompleted[i] = PlayerPrefs.GetInt($"Mission_{i}_Complete", 0) == 1;
        }

        ShowMission(currentIndex);
    }

    void Update()
    {
        // R 키를 누르면 초기화
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearMissionProgress();
        }
    }


    private IEnumerator ShowIntroSpeechBubble()
    {
        dimmedBackground.SetActive(true);

        string[] introLines = new string[]
        {
       "이곳은 미션의 시작점이야.",
        "총 4개의 미션이 준비되어 있어!",
        "미션을 완료하면 퍼즐 조각을 획득할 수 있어.",
        "그럼 퍼즐 조각을 획득해볼까?"
        };

        GameObject bubble = Instantiate(speechBubblePrefab, speechBubbleAnchor.position, Quaternion.identity, speechBubbleAnchor);
        TMP_Text text = bubble.GetComponentInChildren<TMP_Text>(true);

        if (text == null)
        {
            Debug.LogWarning("⚠ TMP_Text가 말풍선 프리팹 안에서 발견되지 않았습니다.");
            yield break;
        }

        bubble.SetActive(true);



        foreach (string line in introLines)
        {
            yield return StartCoroutine(TypeText(text, line));
            yield return new WaitForSeconds(1.5f);
        }

        Destroy(bubble); // 또는 bubble.SetActive(false);
        dimmedBackground.SetActive(false);
    }

    private IEnumerator TypeText(TMP_Text text, string fullText, float charDelay = 0.05f)
    {
        text.text = "";
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(charDelay);
        }
    }

    public void ShowMission(int index)
    {
        Debug.Log($"ShowMission 호출됨 - index: {index}");
        Debug.Log($"[ShowMission] currentIndex: {index}, missionCompleted[{index}] = {missionCompleted[index]}");

        titleText.text = titles[index];
        descriptionText.text = descriptions[index];
        emojiImg.sprite = emojiSprites[index];

        bool isCompleted = PlayerPrefs.GetInt($"Mission_{index}_Complete", 0) == 1;
        puzzleStatusText.text = isCompleted ? "퍼즐 획득 완료 " : "퍼즐 획득 필요 ";

        if (arriveButton != null)
        {
            arriveButton.SetActive(index == 0);
        }
    }

    public void NextMission()
    {
        currentIndex = (currentIndex + 1) % titles.Length;
        ShowMission(currentIndex);
    }

    public void CompleteMission(int index)
    {
        if (index < 0 || index >= missionCompleted.Length) return;

        missionCompleted[index] = true;
        Debug.Log($"[CompleteMission] index: {index}, missionCompleted[{index}] = {missionCompleted[index]}");

        PlayerPrefs.SetInt($"Mission_{index}_Complete", 1);
        PlayerPrefs.SetInt("ShowPuzzlePopup", 1);
        PlayerPrefs.Save();
        Debug.Log($"[Scan] 저장된 Mission_1_Complete: {PlayerPrefs.GetInt("Mission_1_Complete")}");

        ShowMission(currentIndex);
    }

    public void OnClickMissionComplete()
    {
        CompleteMission(currentIndex);
    }

    public void OnClickMission1Arrived()
    {
        CompleteMission(0);
    }

    // 초기화
    private void ResetMissionsAtStart()
    {
        for (int i = 0; i < missionCompleted.Length; i++)
        {
            PlayerPrefs.SetInt($"Mission_{i}_Complete", 0);
            missionCompleted[i] = false;
        }
        PlayerPrefs.Save();
    }

    // 개발용 전체 초기화 함수 (R 키로로 연결)
    public void ClearMissionProgress()
    {
        PlayerPrefs.DeleteKey("MissionsInitialized");

        for (int i = 0; i < missionCompleted.Length; i++)
        {
            PlayerPrefs.DeleteKey($"Mission_{i}_Complete");
        }

        PlayerPrefs.Save();

        Debug.Log("모든 미션 상태 초기화");

        for (int i = 0; i < missionCompleted.Length; i++)
        {
            missionCompleted[i] = false;
        }

        ShowMission(currentIndex);

        StartCoroutine(ShowIntroSpeechBubble());
    }
}
