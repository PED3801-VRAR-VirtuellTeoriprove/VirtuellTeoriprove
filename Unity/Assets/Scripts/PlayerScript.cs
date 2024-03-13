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
    public GameObject q1Canvas;
    public TMP_Text scoreNumber;
    public int score;
    void Start()
    {
        // Hide the Multiple Choice Canvas
        q1Canvas.SetActive(false);
        q1Canvas.GetComponent<MultipleChoiceMenu>().correctAnswerEvent.AddListener(CorrectAnswer);
        q1Canvas.GetComponent<MultipleChoiceMenu>().wrongAnswerEvent.AddListener(WrongAnswer);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided into {other.gameObject.tag}");
        // If the player enters the trigger area
        switch(other.gameObject.tag)
        {
            // case "TriggerPlane1":
            //     MultipleChoiceMenu canvasScript = q1Canvas.GetComponent<MultipleChoiceMenu>();
            //     // Show the Multiple Choice Canvas
            //     other.gameObject.SetActive(false);
            //     q1Canvas.SetActive(true);

            //     canvasScript.SetQuestion("What is the capital of France?");
            //     canvasScript.SetAnswers(new string[] {"Paris", "London", "Berlin", "Madrid"});
            //     canvasScript.SetCorrectAnswer(0);
            //     break;
            case "TriggerPlaneQ1":
                Debug.Log("TriggerPlaneQ1 Collided");
                MultipleChoiceMenu canvasScript = q1Canvas.GetComponent<MultipleChoiceMenu>();
                other.gameObject.SetActive(false);
                q1Canvas.SetActive(true);

                canvasScript.SetQuestion("Q1: Hva burde du gjøre i denne situasjonen?");
                canvasScript.SetAnswers(new string[] {"Kjøre til høyre", "Kjøre til venstre", "Vente", "Kjøre rett fram"});
                canvasScript.SetCorrectAnswer(2);
                break;
            case "TriggerPlaneQ2":
                Debug.Log("TriggerPlaneQ2 Collided");
                MultipleChoiceMenu canvasScript2 = q1Canvas.GetComponent<MultipleChoiceMenu>();
                other.gameObject.SetActive(false);
                q1Canvas.SetActive(true);

                canvasScript2.SetQuestion("Q2: Hva burde du gjøre i denne situasjonen?");
                canvasScript2.SetAnswers(new string[] {"Kjøre til høyre", "Kjøre til venstre", "Vente", "Kjøre rett fram"});
                canvasScript2.SetCorrectAnswer(2);
                break;
        }
        
    }

    public void CorrectAnswer()
    {
        Debug.Log("From player: Correct Answer");
        q1Canvas.SetActive(false);
        score++;
        
        scoreNumber.text = "" + score;

    }

    public void WrongAnswer()
    {
        // TODO: Do some instructive shit
        Debug.Log("From player: Wrong Answer");
        q1Canvas.SetActive(false);

    }
}
