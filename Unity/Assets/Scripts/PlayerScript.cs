using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Get UI Multiple Choice Canvas object
    public GameObject multipleChoiceCanvas;
    void Start()
    {
        // Hide the Multiple Choice Canvas
        multipleChoiceCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        // If the player enters the trigger area
        if (other.gameObject.tag is "TriggerPlane1")
        {
            // Show the Multiple Choice Canvas
            other.gameObject.SetActive(false);
            multipleChoiceCanvas.SetActive(true);
        }
    }
}
