using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public SceneChanger sceneChanger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            sceneChanger.MainMenuScene();
        }
    }
}
