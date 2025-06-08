using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClubListManager : MonoBehaviour
{
    [Header("Prefab & Container")]
    [SerializeField] private GameObject clubItemPrefab; // 프리팹 연결
    [SerializeField] private Transform contentTransform; // ScrollView Content 연결

    private void Start()
    {
        // 씬이 시작되면 자동으로 즐겨찾기 목록 출력
        PopulateFavoriteClubs();
        Debug.Log("[ClubListManager] 즐겨찾기 목록 로딩됨");
    }

    /// 즐겨찾기 목록을 전체 출력
    public void PopulateFavoriteClubs()
    {
        Debug.Log("[ClubListManager] 즐겨찾기 클럽 개수: " + FavoriteClubStore.Favorites.Count);

        // 기존 내용 제거 (중복 방지)
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // 즐겨찾기 클럽들 동적으로 생성
        foreach (var club in FavoriteClubStore.Favorites)
        {
            if (club != null)
            {
                Debug.Log("[ClubListManager] 클럽 추가: " + club.clubName);
                AddClubItem(club);
            }
        }
    }

    /// 개별 클럽 아이템을 생성
    public void AddClubItem(ClubData club)
    {
        GameObject newItem = Instantiate(clubItemPrefab, contentTransform);

        TMP_Text titleText = newItem.transform.Find("TextGroup/TitleText").GetComponent<TMP_Text>();
        titleText.text = club.clubName;

        TMP_Text descText = newItem.transform.Find("TextGroup/DescText").GetComponent<TMP_Text>();
        descText.text = club.shortInfo;

        Image icon = newItem.transform.Find("Image").GetComponent<Image>();
        icon.sprite = LoadClubSprite(club.clubName); // 여전히 이름 기반으로 스프라이트 로딩

        Button deleteButton = newItem.transform.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(() =>
        {
            FavoriteClubStore.RemoveFavorite(club); // ← 이제 club 사용 가능
            Destroy(newItem);
        });
    }

    /// 클럽 이름으로 Sprite 로드
    private Sprite LoadClubSprite(string clubName)
    {
        string filename = "";

        switch (clubName)
        {
            case "이화태권": filename = "EwhaTaekwon"; break;
            case "이콕": filename = "EwhaEcok"; break;
            case "이화주짓수": filename = "EwhaJujitsu"; break;
            case "이화검도부": filename = "EwhaKendo"; break;
            case "뷰할로": filename = "EwhaViewhalloo"; break;
            default: filename = "sample_icon"; break;
        }

        Sprite sprite = Resources.Load<Sprite>("ClubIcons/" + filename);
        if (sprite == null)
            Debug.LogWarning("[ClubListManager] 이미지 로드 실패: " + filename);

        return sprite;
    }

    /// 샘플 버튼 테스트용
    // ClubListManager.cs
    public void OnClick_AddSampleClub()
    {
        ClubData dummyClub = ScriptableObject.CreateInstance<ClubData>();
        dummyClub.clubName = "이화태권";
        dummyClub.shortInfo = "더미 설명입니다.";
        Sprite dummyIcon = LoadClubSprite(dummyClub.clubName);

        FavoriteClubStore.AddFavorite(dummyClub);
        PopulateFavoriteClubs(); // 새로고침
    }
}