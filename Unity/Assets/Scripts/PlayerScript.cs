using System;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Answer
{
    public String text;
    public bool isCorrect;
    public Sprite image;
    public String feedback;
}
[Serializable]
public class TheoryProblem
{
    public String question;
    public GameObject triggerPlane;
    public Answer[] answers;
}

public class PlayerScript : MonoBehaviour
{
    public GameObject qCanvas;
    
    public GameObject[] answerButtons;
    private TMP_Text questionText;

    public GameObject feedbackPanelCorrect;
    private TMP_Text feedbackTextCorrect;

    public GameObject feedbackPanelWrong;
    private TMP_Text feedbackTextWrong;

    public TheoryProblem[] problems;
    
    private TheoryProblem get_problem(GameObject triggerPlane)
    {
        for (int i = 0; i < problems.Length; i++)
        {
            if (problems[i].triggerPlane.GetInstanceID() == triggerPlane.GetInstanceID())
            {
                return problems[i];
            }
        }
        return null; // TODO: Throw or return error
    }

    void Start()
    {
        // Hide the Multiple Choice Canvas
        qCanvas.SetActive(false);
        questionText = qCanvas.GetNamedChild("Question").GetNamedChild("qText").GetComponent<TMP_Text>();

        feedbackTextCorrect = feedbackPanelCorrect.GetNamedChild("Text").GetComponent<TMP_Text>();
        feedbackTextWrong = feedbackPanelWrong.GetNamedChild("Text").GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the plane trigger for the problems
        var problem = get_problem(other.gameObject);
        if (problem != null)
        {
            other.gameObject.SetActive(false);
            if (problem.answers.Length != 4)
            {
                Debug.LogError("Answers array must have 4 elements");
                return;
            }
            qCanvas.SetActive(true);
            questionText.text = problem.question;
            
            for (int i = 0; i < problem.answers.Length; ++i)
            {
                Answer curAnswer = problem.answers[i];
                answerButtons[i].GetNamedChild("Text").GetComponent<TMP_Text>().text = curAnswer.text;
                answerButtons[i].GetNamedChild("Image").GetComponent<Image>().sprite = curAnswer.image;
                answerButtons[i].GetComponent<Button>().onClick.AddListener(delegate { CheckAnswer(curAnswer); });
            }
        }
    }

    public void CheckAnswer(Answer answer)
    {
        qCanvas.SetActive(false);

        if (answer.isCorrect)
        {
            Debug.Log("From player: Correct Answer");
            feedbackPanelWrong.SetActive(false);
            feedbackTextCorrect.text = answer.feedback;
            feedbackPanelCorrect.SetActive(true);
        }
        else
        {
            Debug.Log("From player: Incorrect Answer");
            feedbackPanelCorrect.SetActive(false);
            feedbackTextWrong.text = answer.feedback;
            feedbackPanelWrong.SetActive(true);
        }
    }
}
