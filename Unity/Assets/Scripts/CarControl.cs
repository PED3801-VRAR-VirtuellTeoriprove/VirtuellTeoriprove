using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    private int state = 0;
    private bool move_active = false;
    private float startTime;
    private float t;
    private const float transitionTime = 2f;
    private Vector3 velocity = Vector3.zero;

    [System.Serializable]
    public class DestinationAndPivot
    {
        public Transform destination;
        public bool isPivot;
        public bool standby;
        public float degrees;
    }

    [System.Serializable]
    public class Question
    {
    public List<DestinationAndPivot> destinationAndPivotPairs;
}

    public List<Question> questions;
    void Update()
    {
        if (move_active && state < questions.Count) {
            t = (Time.time - startTime) / transitionTime;
            List<DestinationAndPivot> destinationAndPivotPairs = questions[state].destinationAndPivotPairs;

            for (int i = 0; i < destinationAndPivotPairs.Count; i++) {
                // Skip this iteration if the transform is null
                if (destinationAndPivotPairs[i].standby == true) {
                    continue;
                }

                if (t > 2*i && t <= 2*(i+1)) {
                    Transform targetPos = destinationAndPivotPairs[i].destination;
                    if (destinationAndPivotPairs[i].isPivot) {
                        float totalRotation = destinationAndPivotPairs[i].degrees;
                        float rotationThisFrame = totalRotation * (Time.deltaTime / (transitionTime * 2));
                        transform.RotateAround(targetPos.position, Vector3.up, -rotationThisFrame);
                        transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
                    }
                    else {
                        transform.position = Vector3.SmoothDamp(transform.position, targetPos.position, ref velocity, transitionTime);
                    }
                }
            }

            if (t > 2*destinationAndPivotPairs.Count){
                move_active = false;
                if (state < questions.Count - 1) {
                    state++;
                    startTime = Time.time;
                }
                else {
                    Debug.Log("Finished all questions");
                }
            }
        }
    }
    public void MoveToNextQ() {
        move_active = true;
        startTime = Time.time;
    }
}
