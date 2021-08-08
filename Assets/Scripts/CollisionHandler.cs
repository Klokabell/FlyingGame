using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

        [SerializeField] float loadDelay;
        [SerializeField] AudioClip crashSound;
        [SerializeField] AudioClip successSound;

        [SerializeField] ParticleSystem crashParticles;
        [SerializeField] ParticleSystem successParticles;


        AudioSource aus;

        bool isTransitioning = false;

       

         //A switch statement is used to determine what happens when the player hits a specifically tagged or untagged object
         
         
    void Start() 
    {
        aus = GetComponent<AudioSource>();
    }     
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning){return;}

        
        switch (other.gameObject.tag)
        {       
                
        case "Friendly":
                Debug.Log("Safe on the Launch Pad");
                break;

                //For the player to be able to finish the level
        case "Finish":
                Debug.Log("You made it!");
                Invoke("LevelComplete", loadDelay);
                break;

                //Reload the level when the player crashes into the environment, uses the ReloadLevel method below
        default:
                Debug.Log("You only went and bloody CRASHED");
                BloodyCrashed();
                
                break;
        }
    }

void LevelComplete()
        {       
                isTransitioning = true;
                aus.Stop();
                aus.PlayOneShot(successSound);
                GetComponent<Movement>().enabled = false;
                successParticles.Play();
                Invoke("NextLevel", 1f);
        }
void BloodyCrashed()
        {       
                isTransitioning = true;
                aus.Stop();
                aus.PlayOneShot(crashSound);                
                GetComponent<Movement>().enabled = false;
                crashParticles.Play();
                Invoke("ReloadLevel", 1.2f);
        }


void ReloadLevel()
        { 
                // Setting the currentSceneIndex means you dont have to
                // write the entire line out everytime, not essential just good practice.
                int currentSceneIndex  = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
                
        }

void NextLevel()
        {       //The scene index starts at 0 so the sum of the scenes is 1 higher than the Index id number
                //So when the number reaches one higher than highest scene index ID no. it restarts from 0.
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                int nextSceneIndex = currentSceneIndex + 1;
                        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
                                {
                                   nextSceneIndex = 0;
                                }
                SceneManager.LoadScene(nextSceneIndex);
        }
}

