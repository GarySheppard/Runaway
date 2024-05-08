using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 180f;
    private float currentTime;
    public TextMeshProUGUI timerText; 
    public GameObject gameover;

    void Start()
    {
        currentTime = totalTime; 
        gameover.SetActive(false);
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        gameover.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
