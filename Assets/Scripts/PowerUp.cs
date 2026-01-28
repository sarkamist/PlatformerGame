using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static Action<PowerUp> OnPowerUpCollected;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnPowerUpCollected?.Invoke(this);

        animator.SetBool("IsPickedUp", true);
    }
}
