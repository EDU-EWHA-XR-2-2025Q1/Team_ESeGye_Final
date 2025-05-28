using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApplyButtonHandler : MonoBehaviour
{
    public void OnClickApply()
    {
        if (ClubSelector.SelectedClub != null && !string.IsNullOrEmpty(ClubSelector.SelectedClub.applyLink))
        {
            Application.OpenURL(ClubSelector.SelectedClub.applyLink);
        }
        else
        {
            Debug.LogWarning("���� ��ũ�� �������� �ʾҽ��ϴ�.");
        }
    }
}
