using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    float delayToLoadScene = 1;
    void OnCollisionEnter(Collision collision){
        switch(collision.collider.tag){
            case "Friendly":
                Debug.Log("Phew, a friendly hit!");
                break;
            case "Obstacle":
                Debug.Log("Oops, hit an obstacle!");
                StartCrashSequence();
                break;
            case "Finish":
                StartNextLevelSequence();
                Debug.Log("Hooray! reached destination");
                break;
            default:
                Debug.Log("Unknown Collision");
                break;
        }
    }
    void StartCrashSequence(){
        GetComponent<RocketController>().enabled = false;
        Invoke(nameof(ReloadLevel), delayToLoadScene);
    }
    void StartNextLevelSequence(){
        GetComponent<RocketController>().enabled = false;
        Invoke(nameof(LoadNextLevel), delayToLoadScene);
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel(){
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex % SceneManager.sceneCountInBuildSettings);
    }
}
