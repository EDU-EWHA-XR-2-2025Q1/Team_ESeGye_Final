using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClosingManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea]
        public string text;
    }

    public List<TutorialStep> steps;
    public GameObject panelTutorial;
    public TextMeshProUGUI tutorialText;
    public Button nextButton;
    public TextMeshProUGUI nextButtonText;

    public float typingSpeed = 0.05f;

    private int currentStep = 0;
    private Coroutine typingCoroutine;

    void Start()
    {
        nextButton.onClick.AddListener(NextStep);
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

        nextButton.gameObject.SetActive(true);
        if (index == 5) // 6번째 스텝
        {
            nextButtonText.text = "메인 화면으로";
        }
        else
        {
            nextButtonText.text = "다음";
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        string textToShow = step.text;

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

    void EndTutorial()
    {
        panelTutorial.SetActive(false);
        // 메인 화면으로 이동하는 코드 추가
        // SceneManager.LoadScene("MainScene"); 
    }
}
