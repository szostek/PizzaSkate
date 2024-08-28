using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
        Destroy(gameObject, 5f); 
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") {
            gameManager.AddPoints(100);
            Destroy(gameObject);
        }
    }
}
