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
                Debug.Log("- " + club.clubName);
            }
        }
    }
}