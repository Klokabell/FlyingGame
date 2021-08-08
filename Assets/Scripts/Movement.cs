using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   

    // These are the fields used to adjust the values in unity in order to test the movement and different effects.
    [SerializeField] float rocketSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem mainThrustL;
    [SerializeField] ParticleSystem mainThrustR;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;
    


    Rigidbody rb;
    AudioSource aus;


    void Start()
    {   
        // This calls on the script to get the rigidbody and audiosource at the beginnning of the game for use in the script.
        rb = GetComponent<Rigidbody>();     
        aus = GetComponent<AudioSource>();
    }

    void Update()
    {
        /* The thrust and turn processes are called with the update method so they are 
        more responsive to player commands and the sound effects run properly
        */
        ProcessThrust();
        ProcessTurn();
    }

    void ProcessThrust()
    {
        // this gets the space key as the button for acceleration. 
       if (Input.GetKey(KeyCode.Space))
        {   
            /* this segment adds force in the forward direction of the ship at a consistant rate and
            multiplies it with rocketSpeed, which can be adjusted in unity. It also plays the 
            thrust visual effects
            */
            rb.AddRelativeForce(Vector3.up * rocketSpeed * Time.deltaTime);
            mainThrustL.Play();
            mainThrustR.Play();

            // If the audiosource isn't playing it plays the thrust sound
            if(!aus.isPlaying)
            {
            aus.PlayOneShot(thrustSound);           
            }

            // Stops the thrust visuals when the player isnt pressing space so the whole clip doesnt play through
            if(!mainThrustL.isPlaying)
            {
                mainThrustL.Stop();
                mainThrustR.Stop();
            }
        }
        

        else
        {
            aus.Stop();
        }

    }

    void ProcessTurn()
    {   
        // Turns the ship left using the turnSpeed variable to adjust it, plays the turn effects too
        // if they're not already
       if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-turnSpeed);
            if (!leftThrust.isPlaying)
            {
                leftThrust.Play();
            }
        }
    

       else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(turnSpeed);
            if (!rightThrust.isPlaying)
            {
                rightThrust.Play();
            }
        }

        // Stops the thrust effects when neither turn button is being pressed
        else
        {
            rightThrust.Stop();
            leftThrust.Stop();
        }
    }
        void ApplyRotation(float rotationThisFrame)
        {
            rb.freezeRotation = true; // freezes the rotation from the physics engine to prevent it impacting the turn
            transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
            rb.freezeRotation = false; // unfreezes the physics rotation when the player releases control, allowing it to affect the rotation again
        }
}
