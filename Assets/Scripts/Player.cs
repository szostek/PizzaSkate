using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scene Refs:")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] GameManager gameManager;
    private new Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private Transform groundCheck;
    private Animator animator;
    public bool isDead = false;
    public bool isGrinding;

    [Header("Jumping:")]
    public bool isGrounded;
    public bool isJumping;
    private float jumpVelocity;
    private float maxJumpHeight = 10f;
    private bool onGroundReset = false;

    public bool hasStartedGame = false;

    private void Awake() 
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        isGrounded = false;
    }

    private void FixedUpdate() 
    {
        if (isDead) {
            DestroyImmediate(rigidbody2D);
            return;
        }

        if (!hasStartedGame) return;
        CheckGrounded();
        
        if (isJumping) {
            animator.SetBool("isJumping", true);
            rigidbody2D.velocity = Vector2.up * jumpVelocity;
            jumpVelocity -= 10 * Time.deltaTime;
            if (jumpVelocity <= 0) {
                isJumping = false;
            }
        } else {
            animator.SetBool("isJumping", false);
        }

        HandleMovement();

        // If player falls through a crack, then he is ded:
        if (!isDead && GetPosition().y < -4f) {
            Die();
        }

        // float angle = Vector2.Angle(transform.up, Vector2.up);
        // if (angle > 60f) {
        //     Die();
        // }

    }

    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayerMask);
        if (isGrinding) {
            isGrounded = true;
        } else {
            isGrounded = colliders.Length > 0;
        }
    }

    private void HandleMovement()
    {
        float moveSpeed = 6f;
        rigidbody2D.velocity = new Vector2(+moveSpeed, rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded) {
            jumpVelocity = maxJumpHeight;
            animator.SetFloat("jumpType", Random.Range(0, 4));
            isJumping = true;
        }
    }

    public void Land()
    {
        isJumping = false;
        onGroundReset = true; // Handle this later

        // Add some points..
        gameManager.AddPoints(10);
    }

    public void StartGrinding()
    {
        isGrinding = true;
        animator.SetFloat("grindType", Random.Range(0, 3));
        animator.SetBool("isGrinding", true);
        // Play particles, calc points, etc.
    }

    public void EndGrind()
    {
        isGrinding = false;
        animator.SetBool("isGrinding", false);
        gameManager.AddPoints(20);
    }

    public void Die()
    {
        isDead = true;
        rigidbody2D.velocity = Vector3.zero;
        Destroy(gameObject);
        // Call game over function from game manager
        gameManager.GameOver();
    }

    public Vector3 GetPosition()
    {
        // Check if player is dead first..
        if (isDead) return Vector3.zero;
        // Otherwise..
        return transform.position;
    }
}
