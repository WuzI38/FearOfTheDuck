using UnityEngine;
using Constants;

public abstract class WorldItem : MonoBehaviour
{
    [SerializeField]
    protected itemType type;
    // Allow setting item type only if it wasn't set before to an existing item type
    public itemType Type {
        get => type;
        set {
            if(type == itemType.Null) {
                type = value;
            }
        }
    }
    protected Player playerScript;
    // Get the refernece to the player at the beginning of the game
    protected void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }
    // If the player collides with an object the object is added to player's inventory and then destroyed if it is a gun
    // If it's a health player gains 1 health instead
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            // Destroy the item after it was picked up
            Destroy(gameObject);
        }
    }
}
