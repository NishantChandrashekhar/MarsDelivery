using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision){
        switch(collision.collider.tag){
            case "Friendly":
                Debug.Log("Phew, a friendly hit!");
                break;
            case "Obstacle":
                Debug.Log("Oops, hit an obstacle!");
                ReloadLevel();
                break;
            case "Finish":
                Debug.Log("Hooray! reached destination");
                break;
            default:
                Debug.Log("Unknown Collision");
                break;
        }
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
