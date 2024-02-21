using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Button toggleButton;
    public GameObject panel;
    void Start()
    {
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
}
