using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    // Start is called before the first frame update
    private int state = 0;
    private bool beginQ1 = false;
    private bool beginQ2 = false;

    private float startTime;
    private float t;
    private const float transitionTime = 2f;
    private Vector3 velocity = Vector3.zero;
    public Transform q1Position;
    public Transform q2Position;
    public Transform q2Pivot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beginQ1) {
            t = (Time.time - startTime) / transitionTime;

            if (t <= 1) {
                transform.position = Vector3.SmoothDamp(transform.position, q1Position.position, ref velocity, transitionTime);
            }
            else {
                beginQ1 = false;
                return;
            }
        }
        else if (beginQ2) {
            t = (Time.time - startTime) / transitionTime;

            if (t <= 1) {
                transform.RotateAround(q2Pivot.position, Vector3.up, -90 * Time.deltaTime / transitionTime);
                // Makes cars orientation to also rotate with left side pointing towards the pivot
                transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
            }
            else if (t <= 3 && t > 1) {
                transform.position = Vector3.SmoothDamp(transform.position, q2Position.position, ref velocity, transitionTime);
            }
            else {
                beginQ2 = false;
                return;
            }
        }
    }

    public void MoveToNextQ() {
        switch (state) {
            case 0:
                MoveToQ1();
                state = 1;
                break;
            case 1:
                MoveToQ2();
                state = 2;
                break;
            default:
                break;
        }
    }

    private void MoveToQ1() {
        Debug.Log("Moving towards Q1");
        beginQ1 = true;
        startTime = Time.time;
    }

    private void MoveToQ2() {
        Debug.Log("Moving towards Q2");
        beginQ2 = true;
        startTime = Time.time;
    }
}
