using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script component for interactable items that the player must pick up
public class ObtainItemInteractable : MonoBehaviour, IInteractable
{
    private bool canInteract = true;
    
    public Item item;
    public SubObjective subObj;

    public void Interact(Interactor interactor)
    {
        Animator anim = interactor.GetComponent<Animator>();
        if ((Input.GetKeyDown(KeyCode.E))
            && (canInteract))
        {
            canInteract = false;

            //anim.SetBool("matchToInteraction", true);
            
            /* Gets the sibling script PlayerController for the Interactor in order to trigger the appropriate animation */
            PlayerController controller = interactor.GetComponent<PlayerController>();
            if (gameObject.CompareTag("LowItem"))
            {
                StartCoroutine(controller.InteractionCoroutine(1));
            }
            else if (gameObject.CompareTag("HighItem"))
            {
                StartCoroutine(controller.InteractionCoroutine(2));
            }
            else
            {
                StartCoroutine(controller.InteractionCoroutine(1));
            }

            //Debug.Log("item picked up");
            /* Adjusts inventory and subobjective conditions for object and disappears */
            //InventoryManager.Instance.Add(item);
            subObj.currValue++;
            Destroy(gameObject, 1.5f);

            string item_name = gameObject.name;
            Debug.Log(item_name+" picked up!");
            if (item_name == "walkie(Clone)")
            {
                
                AudioManager.instance.PlaySound("pickup_radio");
            }
            else if (item_name =="cannedfood(Clone)")
            {
                AudioManager.instance.PlaySound("pickup_cannedfood");
            }
            else if (item_name =="firstaid(Clone)")
            {
                AudioManager.instance.PlaySound("pickup_medkit");
            }
            else if (item_name =="waterbottle(Clone)")
            {
                AudioManager.instance.PlaySound("pickup_waterbottle");
            }
            
        }
    }
}