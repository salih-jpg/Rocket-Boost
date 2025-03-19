using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100;
    [SerializeField] float rotationStrength = 100;

    AudioSource audioSource;
    Rigidbody rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if(!audioSource.isPlaying){
                audioSource.Play();
            }
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1f, Time.deltaTime * 2f);
        }else{

              // Smoothly reduce volume to 0
        audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 3f);

        // Stop the audio completely when volume is very low
        if (audioSource.volume <= 0.01f)
        {
            audioSource.Stop();
        }
        }
    }

    private void ProcessRotation(){
        
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0){
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime);
            rb.freezeRotation = false;
        }else if (rotationInput > 0){
            rb.freezeRotation = true;
            transform.Rotate(Vector3.back * rotationStrength * Time.fixedDeltaTime);
            rb.freezeRotation = false;
        }
    }
}
