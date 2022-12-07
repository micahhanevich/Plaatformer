using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Internally Defined")]
    protected Rigidbody rbody;

    public static Player ActivePlayer;

    void Start()
    {
        ActivePlayer = this;
        rbody = GetComponent<Rigidbody>();
    }

    public void Retry()
    {
        transform.position = RetryPoint.ActiveRetryPoint.transform.position;
        rbody.velocity = Vector3.zero;
        PauseMenuHandler.ActivePauseMenu.GetComponent<PauseMenuHandler>().Unpause();
    }

    public void Restart()
    {
        PauseMenuHandler.ActivePauseMenu.GetComponent<PauseMenuHandler>().Unpause();
        SceneManager.LoadSceneAsync(sceneName: "Level_0");
    }
}
