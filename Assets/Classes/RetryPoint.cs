using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryPoint : MonoBehaviour
{
    public static RetryPoint ActiveRetryPoint;
    void Start()
    {
        ActiveRetryPoint = this;
    }
}
