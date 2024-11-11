using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIPanel : MonoBehaviour
{
    public GameObject[] hearts;

    PlayerMovement player;
    PlayerStats playerStats;
    private int previousLivesLeft;

    public void AssignPlayer(int index)
    {
        StartCoroutine(AssignPlayerDelay(index));
    }

    IEnumerator AssignPlayerDelay(int index)
    {
        yield return new WaitForSeconds(0.01f);
        player = GameManager.instance.playerList[index].GetComponent<PlayerInputHandler>().controller;
        playerStats = player.GetComponent<PlayerStats>();

        SetUpInfoPanel();

    }

    void Update()
    {
        if (playerStats != null && previousLivesLeft != playerStats.livesLeft)
        {
            previousLivesLeft = playerStats.livesLeft;
            SetUpInfoPanel();
        }
    }

    void SetUpInfoPanel()
    {
        if (playerStats.livesLeft == 2)
        {
            hearts[5].SetActive(true);
            hearts[4].SetActive(false);
        }        
        if (playerStats.livesLeft == 1)
        {
            hearts[3].SetActive(true);
            hearts[2].SetActive(false);
        }        
        if (playerStats.livesLeft == 0)
        {
            hearts[1].SetActive(true);
            hearts[0].SetActive(false);
        }
    }
}
