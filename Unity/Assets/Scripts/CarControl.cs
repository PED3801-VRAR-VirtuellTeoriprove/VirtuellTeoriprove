using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    private int state = 0;
    private bool move_active = false;
    private bool actionIsActive = false;
    private float startTime;
    private float t;
    private float lastRotation;
    private Vector3 velocity = Vector3.zero;

    [System.Serializable]
    public class MovementProperties
    {
        public Transform destination;
        public bool isPivot;
        public float degrees;
        public float duration;
        public bool standby;
    }

    [System.Serializable]
    public class Action
    {
        public List<MovementProperties> movementProperties;
    }

    public List<Action> action_sequences;
    void Update()
    {
        if (move_active && state < action_sequences.Count) {
            t = Time.time - startTime;
            List<MovementProperties> currentMovesProps = action_sequences[state].movementProperties;

            float timePassed = 0;
            for (int i = 0; i < currentMovesProps.Count; i++) {
                float actionStartTime = timePassed;
                float actionEndTime = timePassed + currentMovesProps[i].duration;
                timePassed = actionEndTime;

                // Skip this iteration if the transform is null or standby is true
                if (currentMovesProps[i].standby == true || currentMovesProps[i].destination == null) {
                    continue;
                }

                if (t > actionStartTime && t <= actionEndTime) {
                    Transform targetPos = currentMovesProps[i].destination;
                    float actionProgress = (t - actionStartTime) / currentMovesProps[i].duration;
                    if (currentMovesProps[i].isPivot) {
                        if (!actionIsActive) {
                            Debug.Log("Action is active, resetting lastRotation");
                            lastRotation = 0;
                            actionIsActive = true;
                        }
                        float rotationSoFar = currentMovesProps[i].degrees * actionProgress;
                        float rotationThisFrame = rotationSoFar - lastRotation;
                        lastRotation = rotationSoFar;

                        Vector3 toTarget = targetPos.position - transform.position;
                        float direction = Vector3.Cross(transform.forward, toTarget).y;

                        if (direction > 0) {
                            // Pivot is to the right, rotate clockwise
                            transform.RotateAround(targetPos.position, Vector3.up, rotationThisFrame);
                        } 
                        else if (direction < 0) {
                            // Pivot is to the left, rotate counterclockwise
                            transform.RotateAround(targetPos.position, Vector3.up, -rotationThisFrame);
                        }
                        else {
                            Debug.Log("Pivot is directly behind the car");
                        }

                        transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
                    } 
                    else {
                        if (!actionIsActive) {
                            Debug.Log("Action is active"); // NOTE: This is printed too many times
                            // velocity = Vector3.zero;
                            actionIsActive = true;
                        }
                        float smoothTime = currentMovesProps[i].duration / 2;
                        transform.position = Vector3.SmoothDamp(transform.position, targetPos.position, ref velocity, smoothTime);
                    }
                }
                else if (t > actionEndTime) {
                    Debug.Log("Action is not active");
                    actionIsActive = false;
                }
            }

            if (t > timePassed){
                move_active = false;
                if (state < action_sequences.Count) {
                    state++;
                }
                else {
                    Debug.Log("Finished all questions");
                }
                return;
            }
        }
    }
    public void MoveToNextQ() {
        if (state < action_sequences.Count) {
            move_active = true;
            startTime = Time.time;
        }
    }
}
