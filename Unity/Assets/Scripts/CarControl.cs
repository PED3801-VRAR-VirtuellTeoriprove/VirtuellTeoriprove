using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CarControl : MonoBehaviour
{
    private int state = 0;
    private bool moveActive = false;
    private bool pivotIsActive = false;
    private float startTime;
    private float t;
    private float lastRotation;
    private Vector3 velocity = Vector3.zero;
    private int recentActionIndex;

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
        if (moveActive && state < action_sequences.Count) {
            t = Time.time - startTime;
            List<MovementProperties> currentMovesProps = action_sequences[state].movementProperties;

            float timePassed = 0;
            for (int i = 0; i < currentMovesProps.Count; i++) {
                // Create timeline w/ timestamps for each action
                // If an action is within current time, execute it
                float actionStartTime = timePassed;
                float actionEndTime = timePassed + currentMovesProps[i].duration;
                timePassed = actionEndTime;

                if (t > actionStartTime && t <= actionEndTime) {
                    Transform targetPos = currentMovesProps[i].destination;
                    float actionProgress = (t - actionStartTime) / currentMovesProps[i].duration;
                    if (currentMovesProps[i].standby || currentMovesProps[i].destination == null) {
                        // Skip this iteration if the transform is null or standby is true
                        continue;
                    }
                    else if (currentMovesProps[i].isPivot) {
                        if (!pivotIsActive || recentActionIndex != i) {
                            Debug.Log("Action is active, resetting lastRotation");
                            lastRotation = 0;
                            pivotIsActive = true;
                        }
                        float rotationSoFar = currentMovesProps[i].degrees * actionProgress;
                        float rotationThisFrame = rotationSoFar - lastRotation;
                        lastRotation = rotationSoFar;

                        Vector3 toTarget = targetPos.position - transform.position;
                        float direction = Vector3.Cross(transform.forward, toTarget).y;

                        if (direction > 0) {
                            // Pivot is to the right, rotate clockwise
                            transform.RotateAround(targetPos.position, Vector3.up, rotationThisFrame);
                            Debug.Log("Rotating clockwise");
                        } 
                        else if (direction < 0) {
                            // Pivot is to the left, rotate counterclockwise
                            transform.RotateAround(targetPos.position, Vector3.up, -rotationThisFrame);
                            Debug.Log("Rotating counterclockwise");
                        }
                        else {
                            Debug.Log("Pivot is directly behind the car");
                        }

                        transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
                        Debug.Log("");
                    }
                    else {
                        pivotIsActive = false;
                        float smoothTime = currentMovesProps[i].duration / 2;
                        transform.position = Vector3.SmoothDamp(transform.position, targetPos.position, ref velocity, smoothTime);
                    }
                    recentActionIndex = i;
                }
            }

            if (t > timePassed){
                moveActive = false;
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
            moveActive = true;
            startTime = Time.time;
        }
    }
}
