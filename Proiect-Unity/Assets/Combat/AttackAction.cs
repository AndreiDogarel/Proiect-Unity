using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour
{
    public Vector2 position;
    public Vector2 direction;
    public List<Attack> attacks = new List<Attack>();

    private bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        attacks.Add(new Attack(10, 10, 0, 10)); //damage, range, cooldown, knockback
                                                //cooldown won't make the cut in the actual form
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        float moveInput = Input.GetAxisRaw("Horizontal");
        if(moveInput > 0)
        {
            isFacingRight = true;
        }
        else if(moveInput < 0)
        {
            isFacingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 attackDirection = GetDirection();
            Debug.Log(attackDirection);
            attacks[0].PerformAttack(position, attackDirection);
        }
    }

    Vector2 GetDirection()
    {
        if (isFacingRight)
        {
            return Vector2.right;
        }
        return Vector2.left;
    }
}
