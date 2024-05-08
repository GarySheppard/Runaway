using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script component for interactable items that the player must hold on
public class HoldInteractable : MonoBehaviour, IInteractable
{
    private bool canStartAudio = true;
    
    public SubObjective subObj;

    public void Interact(Interactor interactor)
    {
        Animator playerAnim = interactor.GetComponent<Animator>();

        if (Input.GetKey(KeyCode.E))
        {
            if (gameObject.CompareTag("Document"))
            {
                if (canStartAudio)
                {
                    canStartAudio = false;
                    AudioManager.instance.PlaySound("documents_reading");
                    Debug.Log("aight");
                }
                playerAnim.SetBool("isInteracting", true);
                playerAnim.SetInteger("interactionState", 3);
            }
            else if (gameObject.CompareTag("Generator"))
            {
                if (canStartAudio)
                {
                    canStartAudio = false;
                    AudioManager.instance.PlaySound("repair_generator_30s");
                }
                
                playerAnim.SetBool("isInteracting", true);
                playerAnim.SetInteger("interactionState", 4);
            }
            else
            {
                playerAnim.SetBool("isInteracting", true);
                playerAnim.SetInteger("interactionState", 4);
            }
            
            /* Adjusts subobjective conditions for object and disappears */
            subObj.currValue++;
        }
        else
        {
            if (gameObject.CompareTag("Generator"))
            {
                canStartAudio = true;
                AudioManager.instance.StopSound("repair_generator_30s");
            }
            playerAnim.SetBool("isInteracting", false);
            playerAnim.SetInteger("interactionState", -1);
        }

        if (subObj.isComplete)
        {
            playerAnim.SetBool("isInteracting", false);
            playerAnim.SetInteger("interactionState", -1);
            Destroy(GetComponent<HoldInteractable>());
        }

    }
}