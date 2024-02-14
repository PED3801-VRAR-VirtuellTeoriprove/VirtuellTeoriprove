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
    public Transform perspective;
    public Transform driverSeat;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(HandleSelectEntered);
    }

    void HandleSelectEntered(SelectEnterEventArgs args)
    {
        // Make the camera a child of the car
        perspective.transform.SetParent(transform);

        // Disable camera collision
        perspective.GetComponentInChildren<Collider>().enabled = false;


        // Set the local position and rotation of the camera to be at the origin of the car
        perspective.transform.localPosition = driverSeat.localPosition;
        perspective.transform.localRotation = driverSeat.localRotation;

        // Disable the camera movement
        perspective.GetComponentInChildren<DynamicMoveProvider>(true).enabled = false;

        // Code to handle "click" event
        Debug.Log("Car was clicked (selected)");
    }
    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(HandleSelectEntered);
    }
}
