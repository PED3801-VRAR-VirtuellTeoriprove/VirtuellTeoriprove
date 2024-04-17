using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarControl : MonoBehaviour
{
    private const float pivotDistanceModifier = 0.8f;
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

    void FixedUpdate()
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
                    distance = currentMovesProps[i].degrees * Mathf.Deg2Rad * currentMovesProps[i].radius * pivotDistanceModifier;
                }
                else {
                    distance = currentMovesProps[i].distance;
                }

                actionEndTime = timePassed + distance / currentMovesProps[i].velocity;
                

                if (t > actionStartTime && t <= actionEndTime) {
                    float actionProgress = (t - actionStartTime) / (distance / currentMovesProps[i].velocity);
                    if (currentMovesProps[i].standby > 0) {
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
                timePassed = actionEndTime;
            }

            if (t > timePassed){
                moveActive = false;
                recentActionIndex = -1;
                if (state < action_sequences.Count) {
                    state++;
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
        float smoothed_progress = progress;

        if (smoothIn && smoothOut) {
            // Smooth in and out - sigmoid function
            float x = (progress - 0.5f) * 10;  // Adjust x to shift the sigmoid function
            smoothed_progress = 1 / (1 + (float)Math.Exp(-x));
        } else if (smoothIn) {
            // Smooth in - ease in cubic using Unity's SmoothStep
            smoothed_progress = Mathf.SmoothStep(0, 1, progress);
        } else if (smoothOut) {
            // Smooth out - ease out cubic using Unity's SmoothStep
            smoothed_progress = Mathf.SmoothStep(1, 0, 1 - progress);
        }

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
