using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float damage;
    private bool initialized;
    private Rigidbody2D rigid;
    private float durability = 10;
    [SerializeField]
    private LayerMask layerMask;
    private Vector3 previousPosition;
    private Vector3 currentPositionRayTo;
    public void SetParams(Vector2 direction, float damage) {
        if(!initialized) {
            this.direction = direction;
            this.damage = damage;
        }
        initialized = true;
    }
    void Awake()
    {
        initialized = false;
        rigid = GetComponent<Rigidbody2D>();
        previousPosition = rigid.position;
    }

    void FixedUpdate()
    {
        if(initialized) {
            rigid.velocity = direction;
        }
        durability -= Time.fixedDeltaTime;
        if(durability < 0) {
            Destroy(gameObject);
        }
    }

    // Check if bullet hit an obstacle using raycast as an additional check to basic unity OnTriggerEnter 
    // This method is not working as good as the default, but i left it here just in case of smoething bad happening
    /*void Update() {
        currentPositionRayTo = (transform.position - previousPosition);
        float range = this.GetComponent<SpriteRenderer>().size.x / 2;
        if (Physics2D.Raycast(previousPosition, currentPositionRayTo, range, layerMask.value))
        {
            Destroy(gameObject);
        }
        previousPosition = rigid.position;
    }*/

    // Collision check
    private void OnTriggerEnter2D(Collider2D collider2D) {
        // Remove an item if it hist a wall or a chest
        if(collider2D.tag == "Wall" || (collider2D.tag == "Chest" && !collider2D.isTrigger)) {
            Destroy(gameObject);
        }
    }
}
