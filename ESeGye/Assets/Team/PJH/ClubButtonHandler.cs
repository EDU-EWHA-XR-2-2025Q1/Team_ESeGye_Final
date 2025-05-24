using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ScanScene에서 Button 클릭 시 실행
public class ClubButtonHandler : MonoBehaviour
{
    public ClubData clubData; // 이 버튼에 해당하는 ClubData 연결

    public void OnClick()
    {
        print("눌림");
        ClubSelector.SelectedClub = clubData;
        UnityEngine.SceneManagement.SceneManager.LoadScene("InfoScene");
    }
}
