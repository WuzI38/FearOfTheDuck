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
}
