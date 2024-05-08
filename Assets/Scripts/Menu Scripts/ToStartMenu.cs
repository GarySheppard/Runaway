using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStartMenu : MonoBehaviour
{
    public void StartGame() 
    {
        AudioManager.instance.PlaySound("melee_bat");
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
