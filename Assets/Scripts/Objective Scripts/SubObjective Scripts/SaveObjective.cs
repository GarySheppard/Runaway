using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObjective : MonoBehaviour
{
    private int numCollidersFound;
    private Collider[] colliders = new Collider[20];

    public LayerMask interactableLayer;
    public SubObjective subObj;

    private void Update()
    {
        numCollidersFound = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 10f, colliders, interactableLayer);
        Debug.Log(numCollidersFound);
        
        if ((numCollidersFound <= 2)
            && (subObj.currValue <= 0))
        {
            subObj.currValue++;
        }
    }
}
