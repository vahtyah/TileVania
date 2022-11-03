using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speedBullet;

    Rigidbody2D rb;
    PlayerMovement player;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        speedBullet *= player.transform.localScale.x;
        print(Mathf.Sign(player.transform.localScale.x));
    }
    private void Update()
    {
        
        rb.velocity = new Vector2(speedBullet, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
