using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Button에 연결되는 함수
public class FavoriteButtonHandler : MonoBehaviour
{
    public void OnClickAddFavorite()
    {
        if (ClubSelector.SelectedClub != null)
        {
            FavoriteClubStore.AddFavorite(ClubSelector.SelectedClub);
            Debug.Log("관심동아리 등록됨: " + ClubSelector.SelectedClub.clubName);

            // 등록된 모든 관심 동아리 이름 출력
            Debug.Log("현재 관심 동아리 목록:");
            foreach (var club in FavoriteClubStore.Favorites)
            {
                if (club == null)
                {
                    Debug.LogWarning("club 객체가 null임");
                }
                else if (string.IsNullOrEmpty(club.clubName))
                {
                    Debug.LogWarning("clubName이 null 또는 빈 문자열임");
                }
                else
                {
                    Debug.Log("- " + club.clubName);
                }
            }
        }
    }
}