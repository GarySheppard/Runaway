using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public Item Item;

    void Pickup(){
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown(){
        Pickup();
    }

}

    // METHOD WILL ONLY WORK IF PREFAB HAS COLLIDER!

  // for the interaction part, unsure if these have conflicts, feel free to override