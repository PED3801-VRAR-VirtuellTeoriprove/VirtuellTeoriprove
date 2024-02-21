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
    public Transform leftController;
    public Transform leftControllerStabilizer;
    public Transform rightController;
    public Transform rightControllerStabilizer;
    public Transform gazeInteractor;
    public Transform driverSeat;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(HandleSelectEntered);
    }

    void HandleSelectEntered(SelectEnterEventArgs args)
    {
        Vector3 cameraLocalPosition = perspective.localPosition;
        Quaternion cameraLocalRotation = perspective.localRotation;
        Vector3 leftControllerLocalPosition = leftController.localPosition;
        Quaternion leftControllerLocalRotation = leftController.localRotation;
        Vector3 rightControllerLocalPosition = rightController.localPosition;
        Quaternion rightControllerLocalRotation = rightController.localRotation;
        Vector3 leftStabilizerLocalPosition = leftControllerStabilizer.localPosition;
        Quaternion leftStabilizerLocalRotation = leftControllerStabilizer.localRotation;
        Vector3 rightStabilizerLocalPosition = rightControllerStabilizer.localPosition;
        Quaternion rightStabilizerLocalRotation = rightControllerStabilizer.localRotation;
        Vector3 gazeInteractorLocalPosition = gazeInteractor.localPosition;
        Quaternion gazeInteractorLocalRotation = gazeInteractor.localRotation;

        // Move the camera to the driver's seat of the car
        perspective.position = driverSeat.position;
        perspective.rotation = driverSeat.rotation;

        // Move the controllers, stabilizers and gaze interactor to their original local positions and rotations relative to the camera
        leftController.position = perspective.TransformPoint(cameraLocalPosition + leftControllerLocalPosition);
        leftController.rotation = perspective.rotation * leftControllerLocalRotation;
        rightController.position = perspective.TransformPoint(cameraLocalPosition + rightControllerLocalPosition);
        rightController.rotation = perspective.rotation * rightControllerLocalRotation;
        leftControllerStabilizer.position = perspective.TransformPoint(cameraLocalPosition + leftStabilizerLocalPosition);
        leftControllerStabilizer.rotation = perspective.rotation * leftStabilizerLocalRotation;
        rightControllerStabilizer.position = perspective.TransformPoint(cameraLocalPosition + rightStabilizerLocalPosition);
        rightControllerStabilizer.rotation = perspective.rotation * rightStabilizerLocalRotation;
        gazeInteractor.position = perspective.TransformPoint(cameraLocalPosition + gazeInteractorLocalPosition);
        gazeInteractor.rotation = perspective.rotation * gazeInteractorLocalRotation;

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
