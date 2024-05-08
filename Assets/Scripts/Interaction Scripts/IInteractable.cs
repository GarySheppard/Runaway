using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface for all interactable objects
public interface IInteractable
{
    public void Interact(Interactor interactor);
}
