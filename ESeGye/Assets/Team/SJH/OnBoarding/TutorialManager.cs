using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea]
        public string text;
        public bool isNameInputStep = false;
    }

    public GameObject panelTutorial;
    public GameObject panelNameInput;

    public TextMeshProUGUI tutorialText;
    public Button nextButton;

    public TMP_InputField nameInputField;
    public Button nameSubmitButton;

    public float typingSpeed = 0.05f;

    private int currentStep = 0;
    private Coroutine typingCoroutine;
    private string playerName = "";

    private List<TutorialStep> steps;

    void Awake()
    {
        // 하드코딩으로 튜토리얼 스텝 초기화
        steps = new List<TutorialStep>()
        {
            new TutorialStep { text = "안녕! \n난 이화여대 신입생의 동아리 탐색을 돕는 안내자,\n동알이야. 잘 부탁해!" },
            new TutorialStep { text = "먼저, 너를 뭐라고 부르면 좋을까?\n이름을 알려줘!", isNameInputStep = true },
            new TutorialStep { text = "이름을 알려주는 코드가 이미 있어요. (여기선 그냥 넘어감)" },
            new TutorialStep { text = "‘동AR이’에서는 동아리 슬로건을 스캔하며 정보를 얻고,\n퍼즐 조각을 모아 미션을 완성할 수 있어.\n퍼즐을 완성하면 작은 보상도 준비되어 있으니 기대해!" },
            new TutorialStep { text = "모든 스캔은 학생문화관에서 진행이 돼. 학문관으로 이동해줘!" },
            new TutorialStep { text = "다양한 조건의 동아리를 스캔하면서 퍼즐 조각을 모아보자!\n 모은 퍼즐 조각은 퍼즐 메뉴에서 확인 가능해." },
            new TutorialStep { text = "마음에 드는 동아리를 찾으면 '스캔'버튼을 누르고 '저장하기'로 저장, '지원하기'로 지원할 수 있어!" },
            new TutorialStep { text = "미션은 미션 목록에서 확인할 수 있어." },
            new TutorialStep { text = "준비됐어?\n이제 진짜 여정을 시작하자, 따라와!" }
        };
    }

    void Start()
    {
        nextButton.onClick.AddListener(NextStep);
        nameSubmitButton.onClick.AddListener(OnNameSubmitted);
        ShowStep(0);
    }

    void ShowStep(int index)
    {
        if (index >= steps.Count)
        {
            EndTutorial();
            return;
        }

        var step = steps[index];

        if (step.isNameInputStep)
        {
            panelTutorial.SetActive(false);
            panelNameInput.SetActive(true);
            nextButton.gameObject.SetActive(false);
            return;
        }

        panelTutorial.SetActive(true);
        panelNameInput.SetActive(false);
        nextButton.gameObject.SetActive(true);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        string textToShow = step.text;

        if (index == 2) // 3번째 스텝에서 플레이어 이름 넣기
        {
            string savedName = PlayerPrefs.GetString("PlayerName", "이름");
            textToShow = $"{savedName}, 만나서 반가워! \n이제 본격적으로 튜토리얼을 시작해볼까?";
        }

        typingCoroutine = StartCoroutine(TypeText(textToShow));
    }

    IEnumerator TypeText(string text)
    {
        tutorialText.text = "";
        foreach (char c in text)
        {
            tutorialText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void NextStep()
    {
        if (currentStep >= steps.Count)
        {
            return;
        }
        if (steps[currentStep].isNameInputStep)
        {
            return;
        }

        currentStep++;
        if (currentStep < steps.Count)
        {
            ShowStep(currentStep);
        }
        else
        {
            EndTutorial();
        }
    }

    void OnNameSubmitted()
    {
        playerName = nameInputField.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerName", playerName);
            currentStep++;
            ShowStep(currentStep);
        }
        else
        {
            Debug.Log("이름을 입력해주세요.");
        }
    }

    void EndTutorial()
    {
        panelTutorial.SetActive(false);
        panelNameInput.SetActive(false);
        SceneManager.LoadScene("MainScene");

        Debug.Log($"튜토리얼 종료! 사용자 이름: {playerName}");
    }
}
