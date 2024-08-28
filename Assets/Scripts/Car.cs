using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Player player;
    private new Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> carSprites;

    private void Awake() 
    {
        player = FindObjectOfType<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = carSprites[Random.Range(0, carSprites.Count)];

        // Destroy the car after some time..
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player") {
            player.Die();
        }
    }

    private void FixedUpdate() 
    {
        if (transform.position.y < -5f) {
            Destroy(gameObject);
            return;
        }
        float moveSpeed = 2f;
        rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
    }
}
