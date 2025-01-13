using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] playerUIPanels;
    public GameObject[] joinMessages;
    public GameObject endPanel;

    private List<int> playerLivesNumber = new List<int>();
    private int joinedPlayers = 0;

    private void Start()
    {
        GameManager.instance.PlayerJoined += PlayerJoinedGame;
        GameManager.instance.PlayerLeft += PlayerLeftGame;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        playerLivesNumber.Clear();

        for (int i = 0; i < GameManager.instance.playerList.Count; i++)
        {
            playerLivesNumber.Add(GameManager.instance.playerList[i].GetComponent<PlayerInputHandler>().controller.GetComponent<PlayerStats>().livesLeft);
        }

        if (joinedPlayers > 1)
        {
            CheckRemainingPlayers();
        }
    }


    void PlayerJoinedGame(PlayerInput playerInput)
    {
        ShowUIPanel(playerInput);
        joinedPlayers++;
    }

    void PlayerLeftGame(PlayerInput playerInput)
    {
        HideUIPanel(playerInput);
    }

    void ShowUIPanel(PlayerInput playerInput)
    {
        playerUIPanels[playerInput.playerIndex].SetActive(true);
        playerUIPanels[playerInput.playerIndex].GetComponent<PlayerUIPanel>().AssignPlayer(playerInput.playerIndex);
        joinMessages[playerInput.playerIndex].SetActive(false);
    }

    void HideUIPanel(PlayerInput playerInput)
    {
        playerUIPanels[playerInput.playerIndex].SetActive(false);
        joinMessages[playerInput.playerIndex].SetActive(true);

    }

    void CheckRemainingPlayers()
    {
        int alivePlayersCounter = 0;
        for (int i = 0; i < playerLivesNumber.Count; i++)
        {
            if (playerLivesNumber[i] > 0)
            {
                alivePlayersCounter++;
            }
        }

        if (alivePlayersCounter < 2)
        {
            Time.timeScale = 0f;
            endPanel.SetActive(true);
        } else
        {
            Time.timeScale = 1f;
            endPanel.SetActive(false);

        }
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
