using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("IsActive", true);
            other.GetComponent<Player>().SetActiveCheckpoint(GetComponent<Checkpoint>());
        }
    }
}
