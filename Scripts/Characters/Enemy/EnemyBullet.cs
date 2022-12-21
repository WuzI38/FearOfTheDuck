using UnityEngine;

public class EnemyBullet : BulletBase
{
    // Collision check
    protected override void OnTriggerEnter2D(Collider2D collider2D) {
        // Remove an item if it hist a wall or a chest
        if(collider2D.tag == "Wall" || (collider2D.tag == "Chest" && !collider2D.isTrigger)) {
            Destroy(gameObject);
        }
        if(collider2D.tag == "Player") {
            Player controller = collider2D.transform.GetComponent<Player>();
            controller.LoseHealth(damage);
            Destroy(gameObject);
        }
    }
}
