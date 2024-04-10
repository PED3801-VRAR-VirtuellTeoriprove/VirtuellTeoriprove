using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MultipleChoiceMenu : MonoBehaviour
{
    public Button toggleButton;
    public GameObject[] answerButtons;
    public GameObject panel;
    public TMP_Text questionText;
    public int correctAnswer;
    public UnityEvent wrongAnswerEvent;
    public UnityEvent correctAnswerEvent;

    void Start()
    {
        if (wrongAnswerEvent == null)
            wrongAnswerEvent = new UnityEvent();
        if (correctAnswerEvent == null)
            correctAnswerEvent = new UnityEvent();
        toggleButton.onClick.AddListener(TogglePanelVisibility);
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

    public void SetAnswers(Answer[] answers)
    {
        if (answers.Length > 4)
        {
            Debug.LogError("Answers array must have less than 4 elements");
            return;
        }
        for (int i = 0; i < answers.Length; i++)
        {
            // Set answer text and sprite
            answerButtons[i].SetActive(true);
            answerButtons[i].GetNamedChild("Text").GetComponent<TMP_Text>().text = answers[i].text;
            answerButtons[i].GetNamedChild("Image").GetComponent<Image>().sprite = answers[i].image;
        }

        for (int i = answers.Length; i < 4; i++)
        {
            // Hide the buttons which are not used here
            answerButtons[i].SetActive(false);
        }
    }

    public void SetCorrectAnswer(int correct)
    {
        correctAnswer = correct;
    }

    public void CheckAnswer(int answer)
    {
        Debug.Log("Clicked: " + answer + "\t Correct: " + correctAnswer);
        if (answer == correctAnswer)
        {
            correctAnswerEvent.Invoke();
        }
        else
        {
            wrongAnswerEvent.Invoke();
        }
    }
}
