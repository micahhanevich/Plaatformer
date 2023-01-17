using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    public static GameObject ActivePauseMenu;

    [SerializeField]
    protected GameObject PauseMenu;

    public bool Paused { get; private set; }

    private void Start()
    {
        ActivePauseMenu = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (!Paused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        Paused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }

    public void Retry()
    {
        Player.activePlayer.Retry();
    }

    public void Restart()
    {
        Player.activePlayer.Restart();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
