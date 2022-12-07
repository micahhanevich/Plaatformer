using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPoint : MonoBehaviour
{
    public static RestartPoint ActiveRestartPoint;
    void Start()
    {
        ActiveRestartPoint = this;
    }
}
