using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script component for the pop-up up that indicates that the player can interact with something they are near
public class InteractionPrompt : MonoBehaviour
{
    public GameObject buttonPrompt;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        //Hides button prompt until future use
        buttonPrompt.SetActive(false);
    }

    void Update()
    {
        //Rotates button prompt to follow the main camera
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
