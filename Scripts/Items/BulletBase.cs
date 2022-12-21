using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected Vector2 direction;
    protected int damage;
    protected bool initialized;
    protected Rigidbody2D rigid;
    protected float durability = 10;
    [SerializeField]
    protected LayerMask layerMask;
    protected Vector3 previousPosition;
    protected Vector3 currentPositionRayTo;
    public void SetParams(Vector2 direction, int damage) {
        if(!initialized) {
            this.direction = direction;
            this.damage = damage;
        }
        initialized = true;
    }
    protected void Awake()
    {
        initialized = false;
        rigid = GetComponent<Rigidbody2D>();
        previousPosition = rigid.position;
    }

    protected void FixedUpdate()
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
    protected virtual void OnTriggerEnter2D(Collider2D collider2D) {
        // Remove an item if it hist a wall or a chest
        if(collider2D.tag == "Wall" || (collider2D.tag == "Chest" && !collider2D.isTrigger)) {
            Destroy(gameObject);
        }
    }
}
