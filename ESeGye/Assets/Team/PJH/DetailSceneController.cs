using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// DetailSceneController.cs
public class DetailSceneController : MonoBehaviour
{
    public TextMeshProUGUI nameText;         // 이름 텍스트
    public TextMeshProUGUI descriptionText;  // 설명 텍스트
    public TextMeshProUGUI tagText;          // 태그 텍스트
    public TextMeshProUGUI shortInfoText; //한줄소개
    public TextMeshProUGUI whereText;         //위치
    public TextMeshProUGUI timeText; //활동시간
    public TextMeshProUGUI activityText;       //주요활동  
    public TextMeshProUGUI conditionText; //활동조건

    void Start()
    {
        var club = ClubSelector.SelectedClub;
        if (club != null)
        {
            nameText.text = club.clubName;
            descriptionText.text = club.description;
            tagText.text = string.Join("  ", club.tags);
            shortInfoText.text = club.shortInfo;
            whereText.text = club.location;
            timeText.text = club.time;
            activityText.text = club.activiry;
            conditionText.text = club.contidition;
        }
    }
}
