using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindTrigger : MonoBehaviour
{
    private Player player;
    private bool isTriggered;

    void Start()
    {
        player = FindObjectOfType<Player>();
        isTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player" && !isTriggered) {
            if (!player.isGrounded) {
                player.StartGrinding();
                isTriggered = true;
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            player.EndGrind();
            isTriggered = false;
        }
    }


}
