<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea]
        public string text;
        public bool isNameInputStep = false;
    }

    public List<TutorialStep> steps;
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

        if (index == 2) // 3번째 스텝일 때만 이름 넣기
        {
            string playerName = PlayerPrefs.GetString("PlayerName", "이름");
            textToShow = $"{playerName}님, 만나서 반가워요! \n이제 본격적으로 튜토리얼을 시작해볼까요?";
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
        if (steps[currentStep].isNameInputStep)
        {
            // 이름 입력 단계에서는 Next 버튼 무효화
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
            return;
        }
    }

    void OnNameSubmitted()
    {
        playerName = nameInputField.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
        {
            currentStep++;
            ShowStep(currentStep);
        }
        else
        {
            // 예외 처리: 이름이 비어있을 때
            Debug.Log("이름을 입력해주세요.");
        }
    }

    void EndTutorial()
    {
        panelTutorial.SetActive(false);
        panelNameInput.SetActive(false);
        Debug.Log($"튜토리얼 종료! 사용자 이름: {playerName}");
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea]
        public string text;
        public bool isNameInputStep = false;
    }

    public List<TutorialStep> steps;
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

        if (index == 2) // 3번째 스텝일 때만 이름 넣기
        {
            string playerName = PlayerPrefs.GetString("PlayerName", "이름");
            textToShow = $"{playerName}님, 만나서 반가워요! \n이제 본격적으로 튜토리얼을 시작해볼까요?";
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
        if (steps[currentStep].isNameInputStep)
        {
            // 이름 입력 단계에서는 Next 버튼 무효화
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
            return;
        }
    }

    void OnNameSubmitted()
    {
        playerName = nameInputField.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
        {
            currentStep++;
            ShowStep(currentStep);
        }
        else
        {
            // 예외 처리: 이름이 비어있을 때
            Debug.Log("이름을 입력해주세요.");
        }
    }

    void EndTutorial()
    {
        panelTutorial.SetActive(false);
        panelNameInput.SetActive(false);
        Debug.Log($"튜토리얼 종료! 사용자 이름: {playerName}");
    }
}
>>>>>>> origin/member/NKDY2
