using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class XRCarClick : MonoBehaviour
{
    private XRSimpleInteractable interactable;
    public Transform bodyOrigin;
    public DynamicMoveProvider bodyMove;
    public Transform driverSeat;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(HandleSelectEntered);
    }

    void HandleSelectEntered(SelectEnterEventArgs args)
    {
        bodyOrigin.position = driverSeat.position;
        bodyOrigin.rotation = driverSeat.rotation;

        ResetChildren(bodyOrigin.gameObject);

        bodyMove.enabled = false;
        // Code to handle "click" event
        Debug.Log("Car was clicked (selected)");
    }

    void ResetChildren(GameObject gameObject)
    {
        foreach (Transform child in gameObject.transform)
        {
            child.localPosition = Vector3.zero;
            child.gameObject.SetActive(true);
        }
    }
    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(HandleSelectEntered);
    }
}
