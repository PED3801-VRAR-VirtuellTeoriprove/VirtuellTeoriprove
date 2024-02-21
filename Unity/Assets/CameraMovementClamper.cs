using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementClamper : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform carTransform;
    private Vector3 driverSeatPosition;
    public float maxDistance = 1f; // Set this to the maximum distance you want
    void Start()
    {
        driverSeatPosition = carTransform.Find("DriverSeat").localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new local position of the camera relative to the car
        Vector3 newLocalPosition = carTransform.InverseTransformPoint(transform.position);

        // Calculate the distance from the new position to the driver's seat
        float distance = Vector3.Distance(newLocalPosition, driverSeatPosition);

        // If the distance exceeds the maximum, clamp the position of the camera
        if (distance > maxDistance)
        {   
            Debug.Log("Clamping");
            Vector3 direction = (newLocalPosition - driverSeatPosition).normalized;
            newLocalPosition = driverSeatPosition + direction * maxDistance;
        }

        // Convert the local position back to world space and update the position of the camera
        transform.position = carTransform.TransformPoint(newLocalPosition);
    }
}
