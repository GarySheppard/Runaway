using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
   Item item;
   Button removeButton;
   public void RemoveItem() {
      InventoryManager.Instance.Remove(item);

      Destroy(gameObject);
   }

   public void AddItem(Item newItem) {
      item = newItem;
   }
/**
   public void UseItem(){
    switch (item.itemType)
    {

        // replacing it with sampleObject1 but need to replace with actual item
      case Item.itemType.sampleObject1:
        Player.Instance.IncreaseHealth(item.value);
        break;
        case Item.itemType.sampleObject:
        Player.Instance.IncreaseExp(item.value);
        break;
  
   }

    RemoveItem();
}
**/
}
