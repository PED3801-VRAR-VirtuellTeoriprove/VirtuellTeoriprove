using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas canvasToHide;
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        // Connect button click to function that closes mainmenu
        closeButton.onClick.AddListener(CloseMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CloseMainMenu() {
        canvasToHide.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        closeButton.onClick.RemoveListener(CloseMainMenu);
    }
}
