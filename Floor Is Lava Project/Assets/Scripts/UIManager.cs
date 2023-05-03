using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public BallController ballController;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] internal GameObject gameOverPanel;
    [SerializeField] internal GameObject victoryPanel;
    [SerializeField] private Button restartButton;

    private bool timeRunning;
    private float currentTime;
    private float bestTime;

    private void Start()
    {
        livesText.text = "Lives: " + ballController.lifeCount.ToString();
        timeRunning = true;
        currentTime = 0f;  // set start time
        bestTime = PlayerPrefs.GetFloat("BestTime", 0);  // get best time from PlayerPrefs (if any)
        if (bestTime > 60 * 60 * 24)
            bestTime = 0f;

        if (bestTime < 0) bestTime = 0f;
        bestTimeText.text = "Best Time: " + bestTime.ToString("F2");

        // set up restart button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartLevel);
        }
    }

    private void Update()
    {
        // update time text
        if (timeRunning)
            currentTime += Time.deltaTime;
        timeText.text = "Time: " + currentTime.ToString("F2");
    }

    public void LoseLife()
    {
        livesText.text = "Lives: " + ballController.lifeCount.ToString();

        // check if player has run out of lives
        if (ballController.lifeCount <= 0)
        {
            // show game over panel
            gameOverPanel.SetActive(true);
            timeRunning = false;
        }
    }

    public void RestartLevel()
    {
        // reload current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ActivateVictoryPanel()
    {
        victoryPanel.SetActive(true);
    }

    public void SetBestTime()
    {
        // check if this is the best time
        if (bestTime == 0 || currentTime < bestTime)
        {
            bestTime = currentTime;
            bestTimeText.text = "Best Time: " + bestTime.ToString("F2");

            // save best time to PlayerPrefs
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }
    }
}