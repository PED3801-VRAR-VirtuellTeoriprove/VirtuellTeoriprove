using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class XRCarClick : MonoBehaviour
{
    public XROrigin xrorigin;
    public GameObject cameraOffset;
    public XRBaseInteractable carInteractable;
    public XRBaseInteractor teleportInteractor;
    public XRRayInteractor teleportRayInteractor;
    public GameObject driverSeat;
    public Canvas startMenu;

    void Start()
    {
        carInteractable.selectEntered.AddListener(HandleSelectEntered);
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Select entered");
        if ((Object)args.interactorObject == (Object)teleportRayInteractor)
        {
            Debug.Log("Teleport interactor selected");
            //Make the XR Rig a child of the car
            xrorigin.transform.parent = carInteractable.transform;

            UnityEngine.Vector3 offset = xrorigin.transform.position - cameraOffset.transform.position;
            xrorigin.transform.position = driverSeat.transform.position + offset;
            xrorigin.transform.rotation = driverSeat.transform.rotation;

            teleportInteractor.enabled = false;
            teleportRayInteractor.enabled = true;
            startMenu.gameObject.SetActive(true);

        }
    }

    private void OnDisable()
    {
        carInteractable.selectEntered.RemoveListener(HandleSelectEntered);
    }
}