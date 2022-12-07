using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wen : MonoBehaviour
{
    [SerializeField]
    protected GameObject WinText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WinText.SetActive(true);
        }
    }
}
