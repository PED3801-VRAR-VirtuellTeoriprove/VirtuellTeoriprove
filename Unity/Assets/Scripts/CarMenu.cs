using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class CarMenu : MonoBehaviour
{
    private Canvas canvasToHide;
    // Start is called before the first frame update
    void Start()
    {
        canvasToHide = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideCanvas() {
        Debug.Log("StartTrigger is called, hiding canvas");
        canvasToHide.gameObject.SetActive(false);
    }

    // void OnDestroy()
    // {
    //     closeButton.onClick.RemoveListener(StartTrigger);
    // }
}
