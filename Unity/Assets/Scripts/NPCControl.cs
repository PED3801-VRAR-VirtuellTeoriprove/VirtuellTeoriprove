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
    public class CarStep
    {
        [Tooltip("List of car indexes to move for this step")]
        public List<int> NPC_cars_to_move;
    }

    [System.Serializable]
    public class Question
    {
        [Tooltip("List of steps for the question to be executed in order")]
        public List<CarStep> NPC_cars_movement_order;
    }
    [Tooltip("List of questions to be executed")]
    public List<Question> questions;

    void Start()
    {
        velocity = new Vector3[cars.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (move_active) {
            t = (Time.time - startTime) / transitionTime;
            List<CarStep> carOrder = questions[state].NPC_cars_movement_order;

            for (int i = 0; i < carOrder.Count; i++) {
                if (t > 2*i && t <= 2*(i+1)) {
                    foreach (int carIndex in carOrder[i].NPC_cars_to_move) {
                        Transform car = cars[carIndex].Find("Car");
                        Transform targetPos = cars[carIndex].Find("Pos" + (state + 1));
                        Debug.Log($"Moving car {carIndex} to {targetPos.position}");
                        car.position = Vector3.SmoothDamp(car.position, targetPos.position, ref velocity[carIndex], transitionTime);
                    }
                }
            }

            if (t > 2*carOrder.Count){
                move_active = false;
                if (state < questions.Count - 1) {
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
        move_active = true;
        startTime = Time.time;
    }
}
