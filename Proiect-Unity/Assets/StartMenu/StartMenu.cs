using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private CounterManager counterManager;

    public Image V1; 
    public Image V2;
    public Image V3;

    private void Start()
    {
        counterManager = FindObjectOfType<CounterManager>();
    }

    public void PlayGame()
    {
        int playerCount = counterManager.GetCurrentValue();
        SetPlayerCount(playerCount);

        int levelIndex = GetActiveMapIndex();
        SceneManager.LoadSceneAsync("Map" + levelIndex);
    }

    public void SetPlayerCount(int playerCount)
    {
        PlayerPrefs.SetInt("PlayerCount", playerCount);
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private int GetActiveMapIndex()
    {
        if (V1.gameObject.activeSelf)
        {
            return 1;
        }
        else if (V2.gameObject.activeSelf)
        {
            return 2;
        }
        else if (V3.gameObject.activeSelf)
        {
            return 3;
        }

        return 1;
    }
}
