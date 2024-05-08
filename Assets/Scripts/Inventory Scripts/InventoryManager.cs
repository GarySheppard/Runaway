using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    [SerializeField]
    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform itemTransform in ItemContent)
        {
            Destroy(itemTransform.gameObject);
        }
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            //Check if RemoveButton is present before trying to access it
            var removeButtonTransform = obj.transform.Find("RemoveButton");
            if (removeButtonTransform != null)
            {
               var removeButton = removeButtonTransform.gameObject;
               if (EnableRemove.isOn)
               {
                   removeButton.SetActive(true);
               }
            }

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }

        SetInventoryItems();
    }


    public void EnableItemsRemove()
    {
       if (EnableRemove.isOn)
       {
           foreach (var item in ItemContent.GetComponentsInChildren<Transform>())
           {
               var removeButtonTransform = item.Find("RemoveButton");
               if (removeButtonTransform != null)
               {
                   var removeButton = removeButtonTransform.gameObject;
                   removeButton.SetActive(true);
               }
           }
       }
       else
       {
           foreach (var item in ItemContent.GetComponentsInChildren<Transform>())
           {
               var removeButtonTransform = item.Find("RemoveButton");
               if (removeButtonTransform != null)
               {
                   var removeButton = removeButtonTransform.gameObject;
                   removeButton.SetActive(false);
               }
           }
       }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
   
}
