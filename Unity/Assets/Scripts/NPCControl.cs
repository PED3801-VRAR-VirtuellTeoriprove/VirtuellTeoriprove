using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : MonoBehaviour
{
    // Start is called before the first frame update
    private int state = 0;
    private bool beginQ1 = false;
    private bool beginQ2 = false;

    private float startTime;
    private float t;
    private const float transitionTime = 2f;
    private Vector3 velocity = Vector3.zero;
    public Transform[] cars;
    public Transform[] q1Positions;
    public Transform[] q2Positions;
    public Transform[] q1Pivots;
    public Transform[] q2Pivots;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beginQ1) {
            t = (Time.time - startTime) / transitionTime;

            if (t <= 1) {
                cars[0].position = Vector3.SmoothDamp(cars[0].position, q1Positions[0].position, ref velocity, transitionTime);
            }
            else {
                beginQ1 = false;
                return;
            }
        }
        else if (beginQ2) {
            t = (Time.time - startTime) / transitionTime;

            if (t <= 1) {
                cars[0].position = Vector3.SmoothDamp(cars[0].position, q2Positions[0].position, ref velocity, transitionTime);
            }
            else if (t <= 3 && t > 2) {
                cars[1].position = Vector3.SmoothDamp(cars[1].position, q2Positions[1].position, ref velocity, transitionTime);
            }
            else if (t > 3){
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
        beginQ1 = true;
        startTime = Time.time;
    }

    private void MoveToQ2() {
        beginQ2 = true;
        startTime = Time.time;
    }
}
