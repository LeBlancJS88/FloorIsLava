using UnityEditor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public BallController ballController;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] internal TMP_Text scoreText;
    [SerializeField] internal TMP_Text scoreCountText;
    [SerializeField] private TMP_Text victoryPanelPlayTime;
    [SerializeField] private TMP_Text victoryPanelBestTime;
    [SerializeField] private TMP_Text slowBonus;
    [SerializeField] private TMP_Text timeBonus;
    [SerializeField] private TMP_Text doubleTimeBonus;
    [SerializeField] private TMP_Text tripleTimeBonus;
    [SerializeField] internal GameObject gameOverPanel;
    [SerializeField] internal GameObject victoryPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private float countDuration = 1f;
    [SerializeField] private float delayBeforeCounting = .5f;

    internal bool timeRunning;
    private float currentTime;
    private float bestTime;
    internal float score;
    internal float scoreMultiplier;

    private void Start()
    {
        scoreMultiplier = 1;

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
        victoryPanelPlayTime.text = "Your Time: " + currentTime.ToString("F2") + " seconds.";
        victoryPanelBestTime.text = "Best Time: " + bestTime.ToString("F2") + " seconds.";

        // Reset the score count text
        scoreCountText.text = "Score: 0";

        // Enable the score count text
        scoreCountText.gameObject.SetActive(true);

        // Enable bonus texts based on time
        if (currentTime >= 150 && currentTime <= 180)
        {
            slowBonus.gameObject.SetActive(true);
        }
        else if (currentTime >= 120 && currentTime < 150)
        {
            timeBonus.gameObject.SetActive(true);
        }
        else if (currentTime >= 90 && currentTime < 120)
        {
            doubleTimeBonus.gameObject.SetActive(true);
        }
        else if (currentTime < 90)
        {
            tripleTimeBonus.gameObject.SetActive(true);
        }

        // Start the counting process
        StartCoroutine(CountingCoroutine());
    }

    private IEnumerator CountingCoroutine()
    {
        // First, wait .2 seconds
        yield return new WaitForSeconds(delayBeforeCounting);

        // Then do the counting
        float scoreCounter;
        float elapsedTime = 0f;
        do
        {
            // Wait for next frame
            yield return new WaitForEndOfFrame();

            // Calculate values
            float countingSpeed = 5000f;
            elapsedTime += Time.deltaTime * countingSpeed;

            // Fixed time, speed varies
            scoreCounter = score * (elapsedTime / countDuration);

            // update the score count text
            scoreCountText.text = "Score: " + Mathf.Round(scoreCounter).ToString();
            scoreText.text = "Score: " + Mathf.Round((score * scoreMultiplier) - scoreCounter).ToString();
        }
        while (scoreCounter < score * scoreMultiplier);

        scoreCountText.text = "Score: " + Mathf.Round(score * scoreMultiplier).ToString();
        scoreText.text = "Score: " + Mathf.Round(score - score).ToString();

        // Start the time countdown
        StartCoroutine(TimeCountdownCoroutine());
    }

    private IEnumerator TimeCountdownCoroutine()
    {
        // Wait for a brief moment before starting the countdown
        yield return new WaitForSeconds(0.5f);

        // Calculate the base points per second
        float basePointsPerSecond = 10f;

        // Adjust the counting speed to control the rate (higher value = faster)
        float countingSpeed = 10f;

        // Check the time and add points accordingly
        while (currentTime > 0)
        {
            float pointsPerSecond = basePointsPerSecond;

            if (currentTime > 180)
            {
                // No points added if time is over 180 seconds
                break;
            }
            else if (currentTime >= 150 && currentTime <= 180)
            {
                pointsPerSecond = 5f;
            }
            else if (currentTime >= 120 && currentTime < 150)
            {
                pointsPerSecond = 10f;
            }
            else if (currentTime >= 90 && currentTime < 120)
            {
                pointsPerSecond = 20f;
            }
            else if (currentTime < 90)
            {
                pointsPerSecond = 30f;
            }

            // Adjust the points per second using the counting speed
            pointsPerSecond *= countingSpeed;

            float pointsToAdd = pointsPerSecond * Time.deltaTime;
            score += pointsToAdd;

            // Update the score count text
            scoreCountText.text = "Score: " + Mathf.Round(score).ToString();

            currentTime -= (Time.deltaTime * countingSpeed);

            // Ensure currentTime doesn't go below 0
            if (currentTime < 0)
            {
                currentTime = 0;
            }

            yield return null;
        }

        // Set the final score
        scoreCountText.text = "Score: " + Mathf.Round(score).ToString();
    }

    public void UpdateScore(float newScore, float newMultiplier)
    {
        score += newScore;
        scoreMultiplier += newMultiplier;
        scoreText.text = "Score: " + (score * scoreMultiplier).ToString();
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