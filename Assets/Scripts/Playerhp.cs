﻿using UnityEngine;

public class Playerhp : MonoBehaviour {

    public float health = 100f;

    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
