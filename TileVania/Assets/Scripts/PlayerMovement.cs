using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] Vector2 deathKick;
    [SerializeField] Transform pointGun;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip coinPickupSFX;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    float currentTimeToNextShoot;
    float maxTimeToNextShoot = .4f;

    public static int arrow = 5;

    public bool isPause = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
        currentTimeToNextShoot = maxTimeToNextShoot;
    }

    private void Update()
    {
        if (isPause) { Time.timeScale = 0; }
        else Time.timeScale = 1;
        if (isAlive && !isPause)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            currentTimeToNextShoot -= Time.deltaTime;
        }
        Die();
    }

    private void Die()
    {
        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) || !isAlive) return;
        isAlive = false;
        animator.SetTrigger("Dying");
        rb.velocity = deathKick;
        GameSession.instance.ProcessPlayerDeat();
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
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Lader")))
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
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || !isAlive || isPause) return;
        if (input.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue inputValue)
    {
        if (!isAlive || isPause) return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (inputValue.isPressed && currentTimeToNextShoot <= 0 && arrow > 0)
        {
            animator.SetTrigger("Shooting");
            StartCoroutine(SpawnBullet());
            currentTimeToNextShoot = maxTimeToNextShoot;
            arrow--;
            GameSession.instance.ChangeArrow();
            return;
        }
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(bullet, pointGun.position, bullet.transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            GameSession.instance.IncementScore();
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(collision.gameObject);
        }
    }
}
