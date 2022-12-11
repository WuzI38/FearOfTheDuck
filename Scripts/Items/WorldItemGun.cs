using UnityEngine;
using Constants;

public class WorldItemGun : WorldItem {
    [SerializeField]
    private GunScriptable gunParams;
    override protected void OnTriggerEnter2D(Collider2D other) {
        // Create new gun object and pass it to player's inventory
        // Gun's type depends on its itemType
        if(other.CompareTag("Player")) {
            Gun gun = null;
            switch(type) {
                case itemType.Health:
                    playerScript.GainHealth(1);
                    break;
                case itemType.Pistol:
                    gun = new Pistol(playerScript, gunParams);
                    break;
                case itemType.Shotgun:
                    gun = new Shotgun(playerScript, gunParams);
                    break;
                case itemType.Rifle:
                    gun = new Rifle(playerScript, gunParams);
                    break;
                default:
                    break;
            }
            if(gun != null) {
                playerScript.AddToPlayerInventory(gun);
            }
            // Destroy the item after it was picked up
            Destroy(gameObject);
        }
    }
}
