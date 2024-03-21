using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;

public class PlayerScript : MonoBehaviour
{
    // Get UI Multiple Choice Canvas object
    private MultipleChoiceMenu canvasScript;
    public GameObject qCanvas;
    public TMP_Text scoreNumber;
    public int score;
    void Start()
    {
        // Hide the Multiple Choice Canvas
        qCanvas.SetActive(false);

        qCanvas.GetComponent<MultipleChoiceMenu>().correctAnswerEvent.AddListener(CorrectAnswer);
        qCanvas.GetComponent<MultipleChoiceMenu>().wrongAnswerEvent.AddListener(WrongAnswer);

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"Collided into {other.gameObject.tag}");
        // If the player enters the trigger area
        switch (other.gameObject.tag)
        {
            case "TriggerPlaneQ1":
                canvasScript = qCanvas.GetComponent<MultipleChoiceMenu>();
                other.gameObject.SetActive(false);
                qCanvas.SetActive(true);

                canvasScript.SetQuestion("Q1: Hva burde du gjøre i denne situasjonen?");
                canvasScript.SetAnswers(new string[] {"Kjøre til høyre", "Kjøre til venstre", "Vente", "Kjøre rett fram"});
                canvasScript.SetCorrectAnswer(2);
                break;
            case "TriggerPlaneQ2":
                canvasScript = qCanvas.GetComponent<MultipleChoiceMenu>();
                other.gameObject.SetActive(false);
                qCanvas.SetActive(true);

                canvasScript.SetQuestion("Q2: Hvem har du vikeplikt for om du skal til høyre?");
                canvasScript.SetAnswers(new string[] {"Venstre", "Begge", "Høyre", "Ingen"});
                canvasScript.SetCorrectAnswer(0);
                break;
            default:
                break;
        }
        
    }

    public void CorrectAnswer()
    {
        Debug.Log("From player: Correct Answer");
        qCanvas.SetActive(false);
        score++;
        
        scoreNumber.text = "" + score;

    }

    public void WrongAnswer()
    {
        // TODO: Do some instructive shit
        Debug.Log("From player: Wrong Answer");
        qCanvas.SetActive(false);

    }
}
