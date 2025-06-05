using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ScannedClubManager : MonoBehaviour
{
    private HashSet<string> scannedClubNames = new();
    private Dictionary<string, int> tagCount = new();

    private bool mission2Done = false;
    private bool mission3Done = false;
    private bool mission4Done = false;

    private void Start()
    {
        // PlayerPrefs에 저장된 상태를 기반으로 내부 상태 초기화
        mission2Done = PlayerPrefs.GetInt("Mission_1_Complete", 0) == 1;
        mission3Done = PlayerPrefs.GetInt("Mission_2_Complete", 0) == 1;
        mission4Done = PlayerPrefs.GetInt("Mission_3_Complete", 0) == 1;
    }

    public void OnScanClub(ClubData club)
    {
        Debug.Log($"[Scan] 클럽 스캔됨: {club.clubName}");
        if (scannedClubNames.Contains(club.clubName))
            return;

        scannedClubNames.Add(club.clubName);

        // Mission 2: 첫 스캔 성공
        if (!mission2Done)
        {
            PlayerPrefs.SetInt("Mission_1_Complete", 1); // Mission 2
            PlayerPrefs.Save();
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
            PlayerPrefs.SetInt("Mission_2_Complete", 1); // Mission 3
            PlayerPrefs.Save();
            mission3Done = true;
        }

        // Mission 4: 모집 중 동아리
        if (!mission4Done && club.isRecruiting)
        {
            PlayerPrefs.SetInt("Mission_3_Complete", 1); // Mission 4
            PlayerPrefs.Save();
            mission4Done = true;
        }
    }
}
