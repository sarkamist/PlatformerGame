using System;
using UnityEngine;

public class PlayerDeathBehaviour : StateMachineBehaviour
{
    public static Action<GameObject> OnPlayerDeath;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("test");
        OnPlayerDeath?.Invoke(animator.gameObject);
    }
}