using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIPanel : MonoBehaviour
{
    public GameObject[] hearts;
    public TextMeshProUGUI healthProcent;

    PlayerMovement player;
    PlayerStats playerStats;
    private int previousLivesLeft;
    private float previousHealthProcent;

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
        if (playerStats != null && (previousLivesLeft != playerStats.livesLeft || previousHealthProcent != playerStats.healthProcent))
        {
            previousLivesLeft = playerStats.livesLeft;
            previousHealthProcent = playerStats.healthProcent;
            SetUpInfoPanel();
        }
    }

    void SetUpInfoPanel()
    {
        healthProcent.enableVertexGradient = true;

        switch (playerStats.livesLeft)
        {
            case 2:
                hearts[5].SetActive(true);
                hearts[4].SetActive(false);
                break;
            case 1:
                hearts[3].SetActive(true);
                hearts[2].SetActive(false);
                break;
            case 0:
                hearts[1].SetActive(true);
                hearts[0].SetActive(false);
                break;
        }

        healthProcent.text = $"WEAKNESS: {previousHealthProcent}%";
    }
}
