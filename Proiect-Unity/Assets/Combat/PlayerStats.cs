using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public float healthProcent;
    public int livesLeft;  

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //healthProcent = 0f;
        livesLeft = 1;
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

    public void OutOfBorder()
    {
        if(livesLeft == 0)
        {
            EliminatePlayer();
        }
        else
        {
            livesLeft--;
            healthProcent = 0;

            //Calls Respawn method after 2 seconds
            //Enough time to let the player wipe his sweaty hands
            Invoke("Respawn", 2);
        }
    }

    void EliminatePlayer()
    {
        Debug.Log(GameManager.instance.playerList.Find(x => x.GetComponent<PlayerInputHandler>().player == rb.gameObject));
        GameManager.instance.KillPlayer(GameManager.instance.playerList.Find(x => x.GetComponent<PlayerInputHandler>().player.Equals(rb.gameObject)));
        rb.gameObject.SetActive(false);
        //Respawn();
        return;
    }

    void Respawn()
    {
        //Reset the oX axis momentum
        rb.velocity = Vector2.zero;
        
        rb.MovePosition(new Vector2(1, 0));
    }
}
