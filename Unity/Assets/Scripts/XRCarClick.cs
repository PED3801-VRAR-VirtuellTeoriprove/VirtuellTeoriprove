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
    public XRBaseInteractable carTeleportAnchor;
    public XRBaseInteractor teleportInteractor;
    public XRRayInteractor rayInteractor;
    public GameObject driverSeat;
    public Canvas startMenu;

    void Start()
    {
        carTeleportAnchor.selectEntered.AddListener(HandleSelectEntered);
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        if ((Object)args.interactorObject == (Object)rayInteractor)
        {
            //Make the XR Rig a child of the car
            xrorigin.transform.parent = carTeleportAnchor.transform;

            UnityEngine.Vector3 offset = xrorigin.transform.position - cameraOffset.transform.position;
            xrorigin.transform.position = driverSeat.transform.position + offset;
            xrorigin.transform.rotation = driverSeat.transform.rotation;

            teleportInteractor.enabled = false;
            carTeleportAnchor.enabled = false;
            rayInteractor.enabled = true;

            startMenu.gameObject.SetActive(true);


        }
    }

    private void OnDisable()
    {
        carTeleportAnchor.selectEntered.RemoveListener(HandleSelectEntered);
    }
}