using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationKill : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1)
        {
            Destroy(animator.gameObject);
        }
    }
}
