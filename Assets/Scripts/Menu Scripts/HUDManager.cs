using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private Slider healthBarAmount = null;
    
    [SerializeField]
    public GameObject player;
    public PlayerStats playerStats;
    [SerializeField]
    public GameObject pauseMenu;
    public GameObject gameOver;

    public Item pistol;
    public Item grenade;
    public Item shuriken;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI nadeText;
    public TextMeshProUGUI shurikenText;
    public GameObject OneBG;
    public GameObject TwoBG;
    public GameObject ThreeBG;
    public GameObject FourBG;

    private void Start() 
    {
        playerStats = player.GetComponent<PlayerStats>();
        Time.timeScale = 1f;
    }

    private void Update()
    {
        //changes the healthBarAmount if the currentHealthValue and newHealthValue are different
        healthBarAmount.value = playerStats.currentHealth;
        //calls pause menu
        if ((Input.GetKeyUp (KeyCode.Escape))
            && (!gameOver.activeInHierarchy))
        {
            AudioManager.instance.PlaySound("melee_bat");
            Pause();
        }
        int ammo = pistol.value;
        ammoText.text = ammo.ToString();
        int nade = grenade.value;
        nadeText.text = nade.ToString();
        int shi = shuriken.value;
        shurikenText.text = shi.ToString();

        ActiveWeapon();
    }

    private void Pause() 
    {
        if (pauseMenu.activeInHierarchy) 
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        } else 
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void RestartButton() 
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }

    public void ToMainMenu() 
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void ActiveWeapon() 
    {
        if (Input.GetKeyDown(1.ToString()))
        {
            OneBG.SetActive(true);
            TwoBG.SetActive(false);
            ThreeBG.SetActive(false);
            FourBG.SetActive(false);
        } else if (Input.GetKeyDown(2.ToString())) 
        {
            OneBG.SetActive(false);
            TwoBG.SetActive(true);
            ThreeBG.SetActive(false);
            FourBG.SetActive(false);
        } else if (Input.GetKeyDown(3.ToString())) 
        {
            OneBG.SetActive(false);
            TwoBG.SetActive(false);
            ThreeBG.SetActive(true);
            FourBG.SetActive(false);
        } else if (Input.GetKeyDown(4.ToString())) 
        {
            OneBG.SetActive(false);
            TwoBG.SetActive(false);
            ThreeBG.SetActive(false);
            FourBG.SetActive(true);
        }
    }
}
