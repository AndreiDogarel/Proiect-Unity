using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public float healthProcent;
    public int livesLeft;

    public bool ableToMove;

    public Rigidbody2D rb;

    private AudioEffects audioEffects;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthProcent = 0f;
        livesLeft = 3;
        ableToMove = true;
        audioEffects = FindObjectOfType<AudioEffects>();
    }

    public void TakeDamage(float attackDamage, float attackKnockback, Vector2 attackDirection)
    {
        float strength = attackKnockback * (healthProcent / 100);
        healthProcent += attackDamage;
        Debug.Log(healthProcent);

        Knockback(attackDirection, strength);

        rb.gameObject.GetComponent<PlayerMovement>().SetCooldown(0.5f);
    }

    void Knockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDirection.normalized * knockbackStrength, ForceMode2D.Impulse);
    }

    public void OutOfBorder()
    {
        audioEffects.PlayDeathSound();
        livesLeft--;

        if (livesLeft == 0)
        {
            EliminatePlayer();
        }
        else
        {
            healthProcent = 0;

            //Calls Respawn method after 2 seconds
            //Enough time to let the player wipe his sweaty hands
            Invoke("Respawn", 2);
        }
    }

    void EliminatePlayer()
    {
        ableToMove = false;
        rb.MovePosition(new Vector2(49.54f, -6.22f));
    }

    void Respawn()
    {
        //Reset the oX axis momentum
        rb.velocity = Vector2.zero;
        
        rb.MovePosition(new Vector2(1, 0));
    }
}
