using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class WorldItemHealth : WorldItem
{
    override protected void OnTriggerEnter2D(Collider2D other) {
        // Add health if health is picked
        if(other.CompareTag("Player")) {
            if(type == itemType.Health) {
                playerScript.GainHealth(1);
            }
            // Destroy the item after it was picked up
            Destroy(gameObject);
        }
    }
}
