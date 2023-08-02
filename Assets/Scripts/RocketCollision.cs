using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision){
        switch(collision.collider.tag){
            case "Friendly":
                Debug.Log("Phew, a friendly hit!");
                break;
            case "Obstacle":
                Debug.Log("Oops, hit an obstacle!");
                break;
            case "Finish":
                Debug.Log("Hooray! reached destination");
                break;
            default:
                Debug.Log("Unknown Collision");
                break;
        }
    }
}
