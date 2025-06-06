using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ScannedClubManager : MonoBehaviour
{
    public MissionCardUI missionUI; // 인스펙터에서 연결

    private HashSet<string> scannedClubNames = new(); // 중복 방지
    private Dictionary<string, int> tagCount = new(); // 태그별 개수

    private bool mission1Done = false;
    private bool mission2Done = false;
    private bool mission3Done = false;
    private bool mission4Done = false;

    // Mission씬과의 연결을 위함
    private void Start()
    {
        if (missionUI == null)
        {
            missionUI = FindObjectOfType<MissionCardUI>();
        }
    }

    public void OnScanClub(ClubData club)
    {
        if (scannedClubNames.Contains(club.clubName))
            return; // 중복 스캔 방지

        scannedClubNames.Add(club.clubName);

        // Mission 2: 첫 스캔 성공

        if (!mission2Done)
        {
            missionUI.CompleteMission(1);
            mission2Done = true;
        }

        // Mission 3: 동일 태그 3개
        foreach (string tag in club.tags)
        {
            if (!tagCount.ContainsKey(tag))
                tagCount[tag] = 0;
            tagCount[tag]++;
        }

        if (!mission3Done && tagCount.Values.Any(count => count >= 3))
        {
            missionUI.CompleteMission(2);
            mission3Done = true;
        }

        // Mission 4: 모집 중 동아리
        if (!mission4Done && club.isRecruiting)
        {
            missionUI.CompleteMission(3);
            mission4Done = true;
        }
    }
}
