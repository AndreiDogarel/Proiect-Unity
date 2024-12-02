using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack 
{
    //Standard class for all the attacks we will think about

    public float damage;
    public float range;
    public float cooldown;
    public float baseKnockback;

    private bool isOnCooldown;

    public LayerMask enemy;

    public Attack(float _damage, float _range, float _cooldown, float _baseKnockback)
    {
        this.damage = _damage;
        this.range = _range;
        this.cooldown = _cooldown;
        this.baseKnockback = _baseKnockback;

        enemy = LayerMask.GetMask("Player");
        isOnCooldown = false;
    }

    public void PerformAttack(Vector2 playerPosition, Vector2 direction)
    {
        if (!isOnCooldown)
        {
            Vector2 offsetPostion = playerPosition + direction;

            RaycastHit2D hit = Physics2D.Raycast(offsetPostion, direction, range);
            
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.GetComponent<PlayerStats>() != null)
                {
                    hit.collider.GetComponent<PlayerStats>().TakeDamage(damage, baseKnockback, direction);
                }
            }
        }
    }
}
