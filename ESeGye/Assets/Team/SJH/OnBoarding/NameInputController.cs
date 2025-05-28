<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button submitButton;

    void Start()
    {
        submitButton.onClick.AddListener(OnNameSubmitted);
    }

    void OnNameSubmitted()
    {
        string playerName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();

            Debug.Log("이름 저장됨: " + playerName);

            // 원하면 다음 행동(예: 튜토리얼 다음 단계 호출)도 여기서 처리 가능
            // 예: TutorialManager.Instance.NextStep(); 또는 씬 이동 등

            // 예시로 패널 끄기
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("이름을 입력해주세요.");
        }
    }
}
=======
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button submitButton;

    void Start()
    {
        submitButton.onClick.AddListener(OnNameSubmitted);
    }

    void OnNameSubmitted()
    {
        string playerName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();

            Debug.Log("이름 저장됨: " + playerName);

            // 원하면 다음 행동(예: 튜토리얼 다음 단계 호출)도 여기서 처리 가능
            // 예: TutorialManager.Instance.NextStep(); 또는 씬 이동 등

            // 예시로 패널 끄기
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("이름을 입력해주세요.");
        }
    }
}
>>>>>>> origin/member/NKDY2
