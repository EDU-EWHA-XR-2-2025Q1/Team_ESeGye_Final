using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// DetailSceneController.cs
public class DetailSceneController : MonoBehaviour
{
    public TextMeshProUGUI nameText;         // �̸� �ؽ�Ʈ
    public TextMeshProUGUI descriptionText;  // ���� �ؽ�Ʈ
    public TextMeshProUGUI tagText;          // �±� �ؽ�Ʈ
    public TextMeshProUGUI shortInfoText; //���ټҰ�
    public TextMeshProUGUI whereText;         //��ġ
    public TextMeshProUGUI timeText; //Ȱ���ð�
    public TextMeshProUGUI activityText;       //�ֿ�Ȱ��  
    public TextMeshProUGUI conditionText; //Ȱ������

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
