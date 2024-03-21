using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCControl : MonoBehaviour
{
    // Start is called before the first frame update
    private int state = 0;
    private bool move_active = false;

    private float startTime;
    private float t;
    private const float transitionTime = 2f;
    private Vector3[] velocity;

    [Tooltip("Array of car containers")]
    public Transform[] cars;
    [System.Serializable]
    public class NPCMovementProperties
    {
        [Tooltip("List of car indexes to move for this step")]
        public List<int> NPC_cars_to_move;
        public float duration;
    }

    [System.Serializable]
    public class Action
    {
        [Tooltip("List of steps for the question to be executed in order")]
        public List<NPCMovementProperties> NPC_cars_movement_order;
    }
    [Tooltip("List of questions to be executed")]
    public List<Action> action_sequences;

    void Start()
    {
        velocity = new Vector3[cars.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (move_active) {
            t = Time.time - startTime;
            List<NPCMovementProperties> carOrder = action_sequences[state].NPC_cars_movement_order;

            float timePassed = 0;
            for (int i = 0; i < carOrder.Count; i++) {
                float actionStartTime = timePassed;
                float actionEndTime = timePassed + carOrder[i].duration;
                timePassed = actionEndTime;

                if (t > actionStartTime && t <= actionEndTime) {
                    if (carOrder[i].NPC_cars_to_move.Count == 0) {
                        continue; // Skip this iteration if the list is empty
                    }
                    foreach (int carIndex in carOrder[i].NPC_cars_to_move) {
                        Transform car = cars[carIndex].Find("Car");
                        Transform targetPos = cars[carIndex].Find("Pos" + (state + 1));
                        // Debug.Log($"Moving car {carIndex} to {targetPos.position}");
                        float smoothTime = carOrder[i].duration / 2;
                        car.position = Vector3.SmoothDamp(car.position, targetPos.position, ref velocity[carIndex], smoothTime);
                    }
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
