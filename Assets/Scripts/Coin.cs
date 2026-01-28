using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value = 5;

    public static Action<Coin> OnCoinCollected;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCoinCollected?.Invoke(this);

        animator.SetBool("IsPickedUp", true);
    }
}
