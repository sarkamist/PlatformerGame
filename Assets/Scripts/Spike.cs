using System;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Death);
            Animator animator = other.gameObject.GetComponent<Animator>();
            animator.SetBool("IsDead", true);
        }
    }
}