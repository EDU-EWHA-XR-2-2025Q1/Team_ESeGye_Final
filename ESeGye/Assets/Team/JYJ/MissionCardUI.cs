using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MissionCardUI : MonoBehaviour
{
    [Header("UI 연결")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI puzzleStatusText;
    public Image emojiImg;

    [Header("미션 이미지")]
    public List<Sprite> emojiSprites;

    private int currentIndex = 0;

    // 미션 완료 상태
    private bool[] missionCompleted = new bool[4]; // 초기값 전부 false

    private readonly string[] titles = {
        "Mission 1: 학문관 도착",
        "Mission 2: 첫 스캔 성공",
        "Mission 3: 동일 태그 동아리 3개",
        "Mission 4: 모집중 동아리 찾기"
    };

    private readonly string[] descriptions = {
        "이화여자대학교의 중앙동아리 정보를 얻을 수 있는 학문관으로 이동하세요.",
        "카메라를 활용해 처음으로 동아리 슬로건을 스캔해보세요.",
        "공통된 태그를 가진 3개의 동아리를 스캔해보고, 내 관심사를 확인해보세요!",
        "현재 모집 중인 동아리를 찾아 스캔하세요. 내 첫 지원은 어떤 동아리까일까요?"
    };

    private readonly bool[] puzzleReceived = {
        true, false, false, false // 예시 상태
    };

    void Start()
    {
        // 저장된 퍼즐 상태 불러오기
        for (int i = 0; i < missionCompleted.Length; i++)
        {
            missionCompleted[i] = PlayerPrefs.GetInt($"Mission_{i}_Complete", 0) == 1;
        }

        ShowMission(currentIndex);
    }

    public void ShowMission(int index)
    {
        titleText.text = titles[index];
        descriptionText.text = descriptions[index];
        emojiImg.sprite = emojiSprites[index];
        puzzleStatusText.text = missionCompleted[index] ? "▣ 퍼즐 획득 완료 " : "ㅁ 퍼즐 획득 필요 ";
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

        // PlayerPrefs에 영구 저장
        PlayerPrefs.SetInt($"Mission_{index}_Complete", 1);
        PlayerPrefs.Save();

        if (index == currentIndex)
        {
            ShowMission(index); // UI 갱신
        }
    }

    public void OnClickMissionComplete()
    {
        CompleteMission(currentIndex);
    }
}
