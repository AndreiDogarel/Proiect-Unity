using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float healthProcent;

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float attackDamage, float attackKnockback, Vector2 attackDirection)
    {
        healthProcent += attackDamage;

        Knockback(attackDirection, attackKnockback * (healthProcent / 100));
    }

    void Knockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        rb.AddForce(knockbackDirection.normalized * knockbackStrength, ForceMode2D.Impulse);
    }
}
