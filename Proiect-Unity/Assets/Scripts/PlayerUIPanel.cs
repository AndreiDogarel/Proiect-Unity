using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIPanel : MonoBehaviour
{
    public GameObject[] hearts;

    PlayerMovement player;
    PlayerStats playerStats;
    private int previousLivesLeft;
    private float previousHealthProcent;
    private Material materialInstance;
    private Image panelImage;

    private void Start()
    {
        panelImage = GetComponent<Image>();

        materialInstance = Instantiate(panelImage.material);
        panelImage.material = materialInstance; // Assign the instance to the panel
    }

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
        if (playerStats.healthProcent >= 100)
        {
            materialInstance.SetFloat("_HealthFactor", 1.0f);

        } else {
            materialInstance.SetFloat("_HealthFactor", playerStats.healthProcent / 100.0f);
        }
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

    }
}
