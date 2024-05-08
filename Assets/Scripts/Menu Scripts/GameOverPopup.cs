using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))] 
public class GameOverPopup : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp (KeyCode.G)) 
        {
            AudioManager.instance.PlaySound("melee_bat");
            if (canvasGroup.interactable) 
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                
                Time.timeScale = 1f;
            } 
            else 
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
        } 
    }

    public void RestartButton() 
    {
        AudioManager.instance.PlaySound("melee_bat");
        SceneManager.LoadScene("Level 1");
    }

    public void ToMainMenu() 
    {
        AudioManager.instance.PlaySound("melee_bat");
        SceneManager.LoadScene("Menu");
    }
}
