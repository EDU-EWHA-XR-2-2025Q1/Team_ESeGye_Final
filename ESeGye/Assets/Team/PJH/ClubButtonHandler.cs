using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ScanScene���� Button Ŭ�� �� ����
public class ClubButtonHandler : MonoBehaviour
{
    public ClubData clubData; // �� ��ư�� �ش��ϴ� ClubData ����

    public void OnClick()
    {
        print("����");
        ClubSelector.SelectedClub = clubData;
        UnityEngine.SceneManagement.SceneManager.LoadScene("InfoScene");
    }
}
