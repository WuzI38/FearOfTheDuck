using System.Collections.Generic;
using UnityEngine;
using Constants;
using UnityEditor;

public class Chest : MonoBehaviour
{
    List<string> itemNames;
    bool canBePicked = false;
    protected Player playerScript;
    // Get the refernece to the player at the beginning of the game
    protected void Awake() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        itemNames = new List<string>();
        playerScript = player.GetComponent<Player>();
        // Get the list of all posiible items
        foreach(string name in System.Enum.GetNames(typeof(itemType))) {
            if(name == "Null" || name == "Health") continue;
            itemNames.Add(name);
        }
    }
    // If the player enters the tile containing chest enable picking the chest and disable if the player leaves that tile
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            canBePicked = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            canBePicked = false;
        }
    }

    void Update() {
        // If player clicks E while standing next to a chest instantiate a random item unowned by the player
        // If the player owns every gun currently available in the game, instantiate the health container instead
        if(Input.GetKeyDown(KeyCode.E) && canBePicked) {
            string path, randItem = "Health";
            List<string> inventoryItems =  playerScript.GetInventoryAsString();
            // If there are no item in the inventory just pick a random item
            if(inventoryItems == null) {
                randItem = itemNames[Random.Range(0, itemNames.Count)];
            }
            else {
                // If you don't posses all of the items remove items in inventory from the list of obtainable item list
                if(inventoryItems.Count < itemNames.Count) {
                    foreach(string name in inventoryItems) {
                        if(itemNames.Contains(name)) {
                            itemNames.Remove(name);
                        }
                    }
                    randItem = itemNames[Random.Range(0, itemNames.Count)];
                }
            }
            // Choose a random item not currently in the inventory
            path = "Assets/Prefabs/" + randItem + "WorldItem.prefab";
            GameObject worldItem = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
            // Instantiate the item + remove the chest
            Instantiate(worldItem, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
