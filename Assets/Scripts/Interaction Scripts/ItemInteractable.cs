using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script component for interactable items that the player must pick up
public class ItemInteractable : MonoBehaviour, IInteractable
{
    public Item item;

    public void Interact(Interactor interactor)
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }
}