using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float damage;
    bool initialized;
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
    }

    void Update()
    {
        if(initialized) {
            transform.position += new Vector3(direction.x, direction.y, 0);
        }
    }
}
