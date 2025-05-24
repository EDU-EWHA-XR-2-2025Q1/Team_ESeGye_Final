using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Button�� ����Ǵ� �Լ�
public class FavoriteButtonHandler : MonoBehaviour
{
    public void OnClickAddFavorite()
    {
        if (ClubSelector.SelectedClub != null)
        {
            FavoriteClubStore.AddFavorite(ClubSelector.SelectedClub);
            Debug.Log("���ɵ��Ƹ� ��ϵ�: " + ClubSelector.SelectedClub.clubName);

            // ��ϵ� ��� ���� ���Ƹ� �̸� ���
            Debug.Log("���� ���� ���Ƹ� ���:");
            foreach (var club in FavoriteClubStore.Favorites)
            {
                Debug.Log("- " + club.clubName);
            }
        }
    }
}