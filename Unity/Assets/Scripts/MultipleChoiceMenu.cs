using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceMenu : MonoBehaviour
{
    public Button toggleButton;
    public Button[] answerButtons;
    public GameObject panel;
    public TMP_Text questionText;
    public int correctAnswer;

    void Start()
    {
        toggleButton.onClick.AddListener(TogglePanelVisibility);
        for (int i = 0; i < 4; i++)
        {
            answerButtons[i].onClick.AddListener(() => CheckAnswer(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TogglePanelVisibility()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void SetQuestion(string question)
    {
        questionText.text = question;
    }

    public void SetAnswers(string[] answers)
    {
        if (answers.Length != 4)
        {
            Debug.LogError("Answers array must have 4 elements");
            return;
        }
        for (int i = 0; i < 4; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = answers[i];
        }
    }

    public void SetCorrectAnswer(int correct)
    {
        correctAnswer = correct;
    }

    void CheckAnswer(int answer)
    {
        if (answer == correctAnswer)
        {
            Debug.Log("Correct Answer");
        }
        else
        {
            Debug.Log("Wrong Answer");
        }
    }
}
