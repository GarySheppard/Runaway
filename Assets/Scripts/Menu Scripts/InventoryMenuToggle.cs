using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simply toggles Inventory on and off, but also
[RequireComponent(typeof(Canvas))]
public class InventoryMenuToggle : MonoBehaviour
{
    private Canvas canvas;
    
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (canvas.enabled == false)
            {
                InventoryManager.Instance.ListItems();
            }
            canvas.enabled = !canvas.enabled;
        }
    }
}
