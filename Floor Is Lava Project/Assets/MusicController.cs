using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip gameMusic;
    public AudioClip gameOverMusic;
    public AudioClip victoryMusic;

    private AudioSource audioSource;

    [SerializeField] private UIManager uiManager;
    private bool isGameOver;
    private bool isVictory;

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Play the game music on loop
        audioSource.clip = gameMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void Update()
    {
        // Check if the player has run out of lives and the GameOver panel appears
        if (!isGameOver && !isVictory && uiManager.gameOverPanel.activeSelf)
        {
            isGameOver = true;

            // Stop the game music and play the GameOver music
            audioSource.Stop();
            audioSource.clip = gameOverMusic;
            audioSource.loop = false;
            audioSource.Play();
        }
        // Check if the player has reached the Goal and the victory panel appears
        else if (!isGameOver && !isVictory && uiManager.victoryPanel.activeSelf)
        {
            isVictory = true;

            // Stop the game music and play the victory music
            audioSource.Stop();
            audioSource.clip = victoryMusic;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
