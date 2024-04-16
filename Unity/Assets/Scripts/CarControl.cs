using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    private int state = 0;
    private bool moveActive = false;
    private bool nextWaiting = false;
    // private bool pivotIsActive = false;
    private float startTime;
    private float previousAngle;
    private float t;
    private int recentActionIndex = -1;
    private Vector3 start = Vector3.zero;
    private Vector3 destination = Vector3.zero;
    private Vector3 pivotPoint = Vector3.zero;

    [System.Serializable]
    public class MovementProperties
    {
        // public Transform start;
        // public Transform destination;
        public float distance;
        
        public bool smoothIn;
        public bool smoothOut;
        public float velocity;
        public bool isPivot;
        public bool isLeft;
        public float degrees;
        public float radius;
        public float standby;
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
                float distance;
                float actionEndTime;
                if (currentMovesProps[i].standby > 0) {
                    actionEndTime = currentMovesProps[i].standby + timePassed;
                    timePassed = actionEndTime;
                    continue;
                }
                else if (currentMovesProps[i].isPivot) {
                    distance = currentMovesProps[i].degrees * Mathf.Deg2Rad * currentMovesProps[i].radius;
                }
                else {
                    // Calculate linear distance between start and destination
                    distance = currentMovesProps[i].distance;
                }
                actionEndTime = timePassed + distance / currentMovesProps[i].velocity * (float)(1 + 0.1 * (currentMovesProps[i].smoothIn ? 1 : 0) + 0.1 * (currentMovesProps[i].smoothOut ? 1 : 0));
                timePassed = actionEndTime;

                if (t > actionStartTime && t <= actionEndTime) {
                    // Transform targetPos = currentMovesProps[i].destination;
                    float actionProgress = (t - actionStartTime) / (distance / currentMovesProps[i].velocity);
                    if (currentMovesProps[i].standby > 0) {
                        // Skip this iteration if not standby
                        continue;
                    }
                    else if (currentMovesProps[i].isPivot) {

                        if (recentActionIndex != i) {
                            if (currentMovesProps[i].isLeft) {
                                // Pivot point to the left
                                pivotPoint = transform.position - transform.right * currentMovesProps[i].radius;
                            }
                            else {
                                // Pivot point to the right
                                pivotPoint = transform.position + transform.right * currentMovesProps[i].radius;
                            }
                            previousAngle = 0;
                        }

                        float smoothedProgress = smoothProgress(actionProgress, currentMovesProps[i].smoothIn, currentMovesProps[i].smoothOut);

                        // Calculate current angle
                        float currentAngle = smoothedProgress * currentMovesProps[i].degrees;
                        float deltaAngle = currentAngle - previousAngle;
                        previousAngle = currentAngle;

                        if (currentMovesProps[i].isLeft) {
                            // Rotate the car around the pivot point
                            transform.RotateAround(pivotPoint, Vector3.up, -deltaAngle);
                        }
                        else {
                            // Rotate the car around the pivot point
                            transform.RotateAround(pivotPoint, Vector3.up, deltaAngle);
                        }
                    }
                    else if (currentMovesProps[i].velocity == 0 || currentMovesProps[i].distance == 0) {
                        continue;
                    }
                    else {
                        // pivotIsActive = false;
                        if (recentActionIndex != i) {
                            start = transform.position; 
                            destination = transform.position + transform.forward * currentMovesProps[i].distance;
                        }

                        float smoothedProgress = smoothProgress(actionProgress, currentMovesProps[i].smoothIn, currentMovesProps[i].smoothOut);

                        transform.position = Vector3.Lerp(start, destination, smoothedProgress);
                    }
                    recentActionIndex = i;
                }
            }

            if (t > timePassed){
                moveActive = false;
                recentActionIndex = -1;
                if (state < action_sequences.Count) {
                    state++;
                }
                else {
                    Debug.Log("Finished all questions");
                }
                if (nextWaiting) {
                    nextWaiting = false;
                    MoveToNextQ();
                }
                return;
            }
        }
    }

    private float smoothProgress(float progress, bool smoothIn, bool smoothOut) {
        // Smooth in/out if selected, else linear
        float smoothed_progress = progress;

        // Smooth in - ease in cubic
        if (progress >= 0 && progress < 0.5f && smoothIn) {
            smoothed_progress = 0.5f * (float)Math.Pow(progress / 0.5f, 3);
        }
        // Smooth out - ease out cubic
        else if (progress <= 1 && progress >= 0.5f && smoothOut) {
            float p = progress - 1;
            smoothed_progress = 1 - 0.5f * (float)Math.Abs(Math.Pow(p / 0.5f, 3));
        }
        Debug.Log("Smoothed progress: " + smoothed_progress + " Progress: " + progress);
        return smoothed_progress;
    }
    public void MoveToNextQ() {
        if (state < action_sequences.Count && !moveActive) {
            moveActive = true;
            startTime = Time.time;
        }
        else if (moveActive) {
            nextWaiting = true;
        }
        else {
            Debug.Log("No more questions");
        }
    }
}
