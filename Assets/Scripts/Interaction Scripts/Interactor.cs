using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script component for the player that allows them to interact with objects
public class Interactor : MonoBehaviour
{
    public Transform interactorTrans;
    public LayerMask interactableLayer;
    public InteractionPrompt interactionPrompt;

    private float interactionRange = 5.0f;
    private Collider[] colliders = new Collider[3];
    //numCollidersFound means the number of Interactable objects in the interactor's trigger area
    private int numCollidersFound;


    // Update is called once per frame
    void Update()
    {
        //Essentially creates a trigger area for the player/interactor wherein any object with a given physics layer
        //(in our case, the Interactable layer) that is in this area will be avaliable for interaction
        numCollidersFound = Physics.OverlapSphereNonAlloc(interactorTrans.position, interactionRange, colliders, interactableLayer);


        if (numCollidersFound > 0)
        {
            var interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactionPrompt.buttonPrompt.SetActive(true);

                //The player either presses or holds E (depending on the interactable) to interacting with an object
                interactable.Interact(this);
            }
        }
        else
        {
            interactionPrompt.buttonPrompt.SetActive(false);
        }
    }

    /* Debugging function; allows you to see the trigger area of the interactor */
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(interactorTrans.position, interactionRange);
    //}
    public float vicinityRadius = 5f; // Adjust the radius as needed

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");
        
        if (other.CompareTag("Scaffolding")) // Replace "YourTag" with the tag of the objects you want to affect
        {
            // Calculate the distance between the player and the other object
            float distance = Vector3.Distance(transform.position, other.transform.position);
            //Debug.Log("Compared");
            //Debug.Log(distance);

            // Check if the other object is within the vicinity radius
            if (distance <= vicinityRadius)
            {
                
                // Check if the other object doesn't already have a Rigidbody
                if (other.GetComponent<Rigidbody>() == null)
                {
                    Collider collider = other.GetComponent<Collider>();
                    // Add Rigidbody component to the other object
                    Rigidbody rb = other.gameObject.AddComponent<Rigidbody>();
                    collider.isTrigger = false;
                    AudioManager.instance.PlaySound("scaffolding_collapse");

                    // Set the collision detection mode to Continuous
                    //rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                }
            }
        }
    }
}
