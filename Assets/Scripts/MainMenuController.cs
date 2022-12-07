using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync(sceneName: "Level_0");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
