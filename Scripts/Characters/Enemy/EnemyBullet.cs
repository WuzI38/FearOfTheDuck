using UnityEngine;

public class EnemyBullet : BulletBase
{
    // Collision check
    protected override void OnTriggerEnter2D(Collider2D collider2D) {
        // Remove an item if it hist a wall or a chest
        if(collider2D.tag == "Wall" || (collider2D.tag == "Chest" && !collider2D.isTrigger)) {
            Destroy(gameObject);
        }
        if(collider2D.tag == "Wall") {
            HealthUI controller = collider2D.transform.GetComponent<HealthUI>();
            // By accident I've set the function's parameter to float whichc is stupid, but I have no motivation to change that
            controller.LoseHealth(damage);
            Destroy(gameObject);
        }
    }
}
