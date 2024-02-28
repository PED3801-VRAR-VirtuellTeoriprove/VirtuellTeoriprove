using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarMenu : MonoBehaviour
{
    private bool triggered = false;
    private float startTime;
    private const float minSpeed = 1f;
    private const float maxSpeed = 10f;
    private const float transitionTime = 10f;
    public Canvas canvasToHide;
    public Button closeButton;
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        // Connect button click to function that closes mainmenu
        closeButton.onClick.AddListener(StartTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) {
            float deltaX;
            float t = (Time.time - startTime) / transitionTime;
            Debug.Log($"Triggered is true, time: {t}");
            if (t < 1) {
                deltaX = Mathf.SmoothStep(minSpeed, maxSpeed, t);
            }
            else if (t < 2) {
                deltaX = Mathf.SmoothStep(maxSpeed, minSpeed, t-1);
            }
            else {
                triggered = false;
                canvasToHide.gameObject.SetActive(true);
                return;
            }
            car.transform.position += new Vector3(deltaX, 0, 0);
        }
    }

    void StartTrigger() {
        Debug.Log("StartTrigger is called");
        canvasToHide.gameObject.SetActive(false);
        triggered = true;
        startTime = Time.time;
    }

    void OnDestroy()
    {
        closeButton.onClick.RemoveListener(StartTrigger);
    }
}
