using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public bool isFacingRight = true, isOnCooldown = false;
    float cooldown = 0.0f;
    List<Attack> attacks = new List<Attack>();
    BoxCollider2D playerCollider;

    [Header("Movement")]
    public float movementSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("Dashing")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.1f;
    bool isDashing;
    bool canDash = true;
    TrailRenderer trailRenderer;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    bool isGrounded;
    bool isOnPlatform;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("Range Attack")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();

        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(playerLayer, playerLayer, true);

        // attacks values to be changed
        attacks.Add(new Attack(10, 3, 0, 30)); // meele attack
        //attacks.Add(new Attack(5, 15, 0, 10)); // range attack
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
        

        GroundCheck();
        Gravity();

        if (horizontalMovement > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0 && isFacingRight)
        {
            Flip();
        }
    }

    // Ground checking
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps;
            animator.SetBool("Jumping", false);
            animator.SetBool("Jake_jumping", false);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("Jumping", true);
            animator.SetBool("Jake_jumping", true);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    // Gravity methods
    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    // Movement methods
    public void Move(InputAction.CallbackContext context)
    {
        if(rb.gameObject.GetComponent<PlayerStats>().ableToMove == true)
        {
            float inputMagnitude = context.ReadValue<Vector2>().x;
            if (inputMagnitude < 0f)
            {
                horizontalMovement = -1f;
            }
            else if (inputMagnitude > 0f)
            {
                horizontalMovement = 1f;
            }
            else
            {
                horizontalMovement = 0f;
            }
            animator.SetBool("Walking", horizontalMovement == 0 ? false : true);
            animator.SetBool("Jake_walking", horizontalMovement == 0 ? false : true);
        }
    }

    // Jumping methods
    public void Jump(InputAction.CallbackContext context)
    {
        if (rb.gameObject.GetComponent<PlayerStats>().ableToMove == true)
        {
            if (jumpsRemaining > 0)
            {
                if (context.performed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                    jumpsRemaining--;
                }
                else if (context.canceled)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0.5f * rb.velocity.y);
                    jumpsRemaining--;
                }
            }
        }
            
    }

    // Dashing methods
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && !isOnCooldown)
        {
            StartCoroutine(DashCoroutine());
            animator.SetBool("Dash", true);
            animator.SetBool("Jake_dash", true);
        }
        else
        {
            animator.SetBool("Dash", false);
            animator.SetBool("Jake_dash", false);
        }
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        
        trailRenderer.emitting = true;
        float dashDirection = isFacingRight ? 1f : -1f;

        rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);
        yield return new WaitForSeconds(dashDuration);

        rb.velocity = new Vector2(0f, rb.velocity.y); //reset horizontal velocity

        isDashing = false;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    // Dropping through platform methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;

        }
    }

    public void Drop(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && isOnPlatform && playerCollider.enabled)
        {
            StartCoroutine(DisablePlayerCollider(0.5f));
        }
    }

    private IEnumerator DisablePlayerCollider(float disableTime)
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(disableTime);
        playerCollider.enabled = true;
    }

    // Attacking methods
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && !isOnCooldown)
        {
            Vector2 attackDirection = isFacingRight ? Vector2.right : Vector2.left;
            Debug.Log(attackDirection);
            attacks[0].PerformAttack(transform.position, attackDirection);
            animator.SetBool("Short", true);
            animator.SetBool("Jake_short", true);

            isOnCooldown = true;
            cooldown = 0.1f;

            Invoke("ResetCooldown", cooldown);
        }
        else
        {
            animator.SetBool("Short", false);
            animator.SetBool("Jake_short", false);
        }
    }

    public void RangeAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isOnCooldown)
        {
            //Vector2 attackDirection = isFacingRight ? Vector2.right : Vector2.left;
            //Debug.Log(attackDirection);
            //attacks[1].PerformAttack(transform.position, attackDirection);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            animator.SetBool("Long", true);
            animator.SetBool("Jake_long", true);

            isOnCooldown = true;
            cooldown = 0.5f;

            Invoke("ResetCooldown", cooldown);
        }
        else
        {
            animator.SetBool("Long", false);
            animator.SetBool("Jake_long", false);
        }
    }

    public void SetCooldown(float _cooldown)
    {
        isOnCooldown = true;
        cooldown = _cooldown;

        Invoke("ResetCooldown", cooldown);
    }

    void ResetCooldown()
    {
        isOnCooldown = false;
        cooldown = 0.0f;
    }
}
