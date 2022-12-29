using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_health : MonoBehaviour, IHitable
{
    public int health = 500;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public void TakeHit(int damage)
    {
        if (isInvulnerable) return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }
}
