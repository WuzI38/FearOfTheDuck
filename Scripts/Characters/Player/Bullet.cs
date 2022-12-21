using UnityEngine;

public class Bullet : BulletBase
{
    // Collision check
    protected override void OnTriggerEnter2D(Collider2D collider2D) {
        // Remove an item if it hist a wall or a chest
        if(collider2D.tag == "Wall" || (collider2D.tag == "Chest" && !collider2D.isTrigger)) {
            Destroy(gameObject);
        }
        if(collider2D.tag == "Enemy") {
            EnemyController controller = collider2D.transform.GetComponent<EnemyController>();
            controller.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
