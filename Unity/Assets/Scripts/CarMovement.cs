using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Transform mainCameraTransform;
    private Vector3 driverSeatPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Find the main camera in the scene and get its Transform component
        mainCameraTransform = Camera.main.transform;

        driverSeatPosition = transform.Find("DriverSeat").localPosition;
        driverSeatPosition = Vector3.Scale(driverSeatPosition, transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        // Set the position of the car to be the same as the position of the main camera
        Vector3 newPosition = mainCameraTransform.position - driverSeatPosition;
        
        // Keep constant x position
        newPosition.x = transform.position.x;
        transform.position = newPosition;
    }
}