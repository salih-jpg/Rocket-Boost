using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    bool isControllable = true;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(!isControllable){
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("it is friendly");
                break;
            case "Fuel":
                Debug.Log("it is fuel");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();    
                break;
        }
    }

    private void StartCrashSequence()
    {
        audioSource.volume = 1f; 
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), delay); // delay 

    }

    private void StartSuccessSequence(){
        audioSource.volume = 1f;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(NextLevel), delay);
    }

    void ReloadLevel (){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void NextLevel (){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene +1;

        if (nextScene == SceneManager.sceneCountInBuildSettings){
            nextScene = 0; 
        }
        SceneManager.LoadScene(nextScene);
        
    }
}
