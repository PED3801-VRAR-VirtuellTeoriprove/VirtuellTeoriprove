using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEditor.PackageManager;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;

[Serializable]
public class Answer
{
    public string text;
    public Sprite image;
}
[Serializable]
public class TheoryProblem
{
    public GameObject triggerPlane;
    public String question;
    public Answer[] answers;
    public int correctAnswer;
    public String wrongAnswerFeedback;
    public String correctAnswerFeedback;
}

public class PlayerScript : MonoBehaviour
{
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
    
    // Get UI Multiple Choice Canvas object
    private MultipleChoiceMenu canvasScript;
    // public GameObject multipleChoiceMenu;
    public GameObject qCanvas;
    public TMP_Text scoreNumber;
    public int score;

    public GameObject feedbackPanelCorrect;
    private TMP_Text feedbackTextCorrect;

    public GameObject feedbackPanelWrong;
    private TMP_Text feedbackTextWrong;

    public TheoryProblem[] problems;

    void Start()
    {
        // Hide the Multiple Choice Canvas
        qCanvas.SetActive(false);

        qCanvas.GetComponent<MultipleChoiceMenu>().correctAnswerEvent.AddListener(CorrectAnswer);
        qCanvas.GetComponent<MultipleChoiceMenu>().wrongAnswerEvent.AddListener(WrongAnswer);

        feedbackTextCorrect = feedbackPanelCorrect.GetNamedChild("Text").GetComponent<TMP_Text>();
        feedbackTextWrong = feedbackPanelWrong.GetNamedChild("Text").GetComponent<TMP_Text>();
        
        canvasScript = qCanvas.GetComponent<MultipleChoiceMenu>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the plane trigger for the problems
        var problem = get_problem(other.gameObject);
        if (problem != null)
        {
            qCanvas.SetActive(true);
            canvasScript.SetQuestion(problem.question);
            canvasScript.SetAnswers(problem.answers);
            canvasScript.SetCorrectAnswer(problem.correctAnswer);

            feedbackTextCorrect.text = problem.correctAnswerFeedback;
            feedbackTextWrong.text = problem.wrongAnswerFeedback;
        }
        
    }

    public void CorrectAnswer()
    {
        Debug.Log("From player: Correct Answer");
        qCanvas.SetActive(false);
        score++;
        
        scoreNumber.text = "" + score;
        
        feedbackPanelCorrect.SetActive(true);

    }

    public void WrongAnswer()
    {
        // TODO: Do some instructive shit
        Debug.Log("From player: Wrong Answer");
        qCanvas.SetActive(false);

        feedbackPanelWrong.SetActive(true);
    }
}
