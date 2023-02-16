using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip smash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem smashParticles;

    AudioSource sound;

    bool isTransistioning = false;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isTransistioning)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("Friendly");
                break;

            case "Finish":
                StartSuccesSequence();
                //Debug.Log("Finish");
                break;

            default:
                StartCrashSequence();
                //Debug.Log("Dead");
                break;
        }
    }

    void StartSuccesSequence()
    {
        isTransistioning = true;
        sound.Stop();
        sound.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransistioning = true;
        sound.Stop();
        sound.PlayOneShot(smash);
        smashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    //loads the current scene when you die
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
