using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    public AudioMixer audioMixer1;
    public AudioMixer audioMixer2;

    private GameManager gameManager;

    void Start()
    {
        pauseMenu.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (gameManager != null)
        {
            foreach (PlayerInput playerInput in gameManager.playerList)
            {
                playerInput.DeactivateInput();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (gameManager != null)
        {
            foreach (PlayerInput playerInput in gameManager.playerList)
            {
                playerInput.ActivateInput();
            }
        }
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer1.SetFloat("MusicVolume", volume);

    }

    public void SetVolumeAudio(float volume)
    {
        audioMixer2.SetFloat("AudioVolume", volume);

    }
}
