using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Map1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
