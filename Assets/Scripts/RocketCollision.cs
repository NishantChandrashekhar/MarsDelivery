using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem finish;

    AudioSource audioSource;
    float delayToLoadScene = 1;

    bool collided = false;
    bool disableCollision = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            disableCollision = !disableCollision;
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collided || disableCollision)
            return;
        switch(collision.collider.tag){
            case "Friendly":
                break;
            case "Obstacle":
                StartCrashSequence();
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                break;
        }
    }
    void StartCrashSequence(){
        // Marking the collide
        collided = true;

        // Disabling the rocket movement
        GetComponent<RocketController>().enabled = false;
        
        // Playing audio
        audioSource.clip = collisionSound;
        if (!audioSource.isPlaying) { audioSource.Play(); }

        // Triggering the explosion particles
        explosion.Play();

        // Invoking the spawn
        Invoke(nameof(ReloadLevel), delayToLoadScene);
    }
    void StartNextLevelSequence(){
        // Marking the collide
        collided = true;

        // Playing the audio
        audioSource.clip = finishSound;
        if (!audioSource.isPlaying) { audioSource.Play(); }

        // Disabling the rocket movement
        GetComponent<RocketController>().enabled = false;

        // Triggering the finish particles
        finish.Play();

        // Invoking the next level
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
