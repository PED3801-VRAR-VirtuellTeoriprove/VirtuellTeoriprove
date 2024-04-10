using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonScript : MonoBehaviour
{
    public GameObject toggledObject;

    public void toggleObject()
    {
        toggledObject.SetActive(!toggledObject.activeSelf);
    }
}
