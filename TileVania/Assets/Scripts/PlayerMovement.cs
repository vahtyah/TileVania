using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float climbSpeed;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    Collider2D cl;
    float gravityScaleAtStart;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    private void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if (!cl.IsTouchingLayers(LayerMask.GetMask("Lader")))
        {
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
            return;
        }

        rb.gravityScale = 0f;
        Vector2 playerVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = playerVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void OnJump(InputValue input)
    {
        if (!cl.IsTouchingLayers(LayerMask.GetMask("Ground")) && !cl.IsTouchingLayers(LayerMask.GetMask("Lader"))) return;
        if (input.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }
}
