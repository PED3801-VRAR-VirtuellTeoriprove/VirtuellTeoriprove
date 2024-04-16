using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class XRCarClick : MonoBehaviour
{
    public XROrigin xrorigin;
    public GameObject cameraOffset;
    public XRBaseInteractable carTeleportAnchor;
    public XRBaseInteractor teleportInteractor;
    public XRRayInteractor rayInteractor;
    public Transform driverSeat;
    public Transform driverSeatSim;
    public GameObject startMenu;
    public GameObject carBody;
    public XRDeviceSimulator XR_DeviceSimulator;
    private MeshCollider bodyCollider;

    void Start()
    {
        carTeleportAnchor.selectEntered.AddListener(HandleSelectEntered);
        bodyCollider = carBody.GetComponent<MeshCollider>();
        if (XR_DeviceSimulator.isActiveAndEnabled)
        {
            driverSeat.position = driverSeatSim.position;
        }
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        if ((Object)args.interactorObject == (Object)rayInteractor)
        {
            //Make the XR Rig a child of the car
            xrorigin.transform.parent = carTeleportAnchor.transform;

            xrorigin.transform.position = driverSeat.position;
            xrorigin.transform.rotation = driverSeat.rotation;

            teleportInteractor.enabled = false;
            carTeleportAnchor.enabled = false;
            rayInteractor.enabled = true;

            startMenu.SetActive(true);


        }
    }

    private void OnDisable()
    {
        carTeleportAnchor.selectEntered.RemoveListener(HandleSelectEntered);
    }
}