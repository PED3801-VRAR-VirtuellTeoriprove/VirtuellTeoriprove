using UnityEngine;  
using UnityEngine.SceneManagement;  
public class SceneChanger: MonoBehaviour {  
    public void Scene1() {  
        SceneManager.LoadScene("Scenario1");  
    }  
    public void Scene2() {  
        SceneManager.LoadScene("Scenario2");  
    }  
    public void MainMenuScene() {  
        SceneManager.LoadScene("MainMenu");
    }
    public void CloseGame() {  
        Application.Quit();  
    }
}   