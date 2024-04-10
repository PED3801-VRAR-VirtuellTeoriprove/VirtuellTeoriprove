using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothCarControl : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;
    private InputDevice device;
    [SerializeField]
    public float maxSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    // Update is called once per frame
    void Update()
    {
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }


    void MoveForward()
    {

    }
}
