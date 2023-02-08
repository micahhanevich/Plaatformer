using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Got here");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("And here!");
            Player.activePlayer.Retry();
        }
    }
}
