using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIPanel : MonoBehaviour
{
    public Text playerLives;
    public Text playerHealth;

    PlayerMovement player;
    PlayerStats playerStats;

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

    void SetUpInfoPanel()
    {
        playerHealth.text = playerStats.healthProcent.ToString("F2");
    }
}
