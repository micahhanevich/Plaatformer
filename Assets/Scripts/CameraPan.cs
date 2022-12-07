using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField]
    protected GameObject Player;

    void Update()
    {
        if (Player.transform.position.x > 0)
        {
            transform.position = new Vector3(Player.transform.position.x, 0, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
    }
}
