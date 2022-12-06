using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class WorldItem : MonoBehaviour
{
    [SerializeField]
    private itemType type;
    // Allow setting item type only if it wasn't set before to an existing item type
    public itemType Type {
        get => type;
        set {
            if(type == itemType.Null) {
                type = value;
            }
        }
    }
    private Player playerScript;
    // Get the refernece to the player at the beginning of the game
    void Awake() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }
    // If the player collides with an object the object is added to player's inventory and then destroyed if it is a gun
    // If it's a health player gains 1 health instead
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            switch(type) {
                case itemType.Health:
                    playerScript.GainHealth(1);
                    break;
                case itemType.Pistol:
                    playerScript.AddToPlayerInventory(itemType.Pistol);
                    break;
                case itemType.Shotgun:
                    playerScript.AddToPlayerInventory(itemType.Shotgun);
                    break;
                case itemType.Rifle:
                    playerScript.AddToPlayerInventory(itemType.Rifle);
                    break;
                default:
                    break;
            }
        }
        // Destroy the item after it was picked up
        Destroy(gameObject);
    }
}
