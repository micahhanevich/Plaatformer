using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Internally Defined")]
    protected Rigidbody2D rbody;

    public static Player activePlayer;

    public Checkpoint ActiveCheckpoint { get; protected set; }

    protected KeyCode RetryKey = KeyCode.R;

    void Start()
    {
        activePlayer = this;
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(RetryKey)) { Retry(); }
    }

    public void Retry()
    {
        if (ActiveCheckpoint is not null) { transform.position = ActiveCheckpoint.transform.position; }
        else { transform.position = RestartPoint.ActiveRestartPoint.transform.position; }
        rbody.velocity = Vector2.zero;
        PauseMenuHandler.ActivePauseMenu.GetComponent<PauseMenuHandler>().Unpause();
    }

    public void Restart()
    {
        PauseMenuHandler.ActivePauseMenu.GetComponent<PauseMenuHandler>().Unpause();
        SceneManager.LoadSceneAsync(sceneName: "Level_0");
    }

    public void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        if (ActiveCheckpoint is not null) { ActiveCheckpoint.GetComponent<Animator>().SetBool("IsActive", false); }
        checkpoint.gameObject.GetComponent<Animator>().SetBool("IsActive", true);
        ActiveCheckpoint = checkpoint.gameObject.GetComponent<Checkpoint>();
    }
}
