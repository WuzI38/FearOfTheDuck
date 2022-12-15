using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class Player : PersistentSingleton<Player>
{
    private Inventory inventory;
    private HealthUI healthUI;
    private Gun gun;
    private GameObject gunPrefab;
    private bool shooting;
    private const int MAXHEALTH = 3;
    override protected void Awake()
    {
        base.Awake();
        inventory = gameObject.AddComponent<Inventory>();
        healthUI = gameObject.AddComponent<HealthUI>(); 
        healthUI.SetStartingHealth(MAXHEALTH);
        gun = null;
        shooting = false;
    }

    public void AddToPlayerInventory(Gun item) {
        inventory.AddItem(item);
    }

    public void GainHealth(int amount) {
        healthUI.GainHealth(amount);
    }

    public List<string> GetInventoryAsString() {
        return inventory.AsString();
    }

    void Update()
    {
        // Enable changing weapons
        if(Input.GetAxis("Mouse ScrollWheel") > 0f ) { // forward 
            inventory.SwapItem(1);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f ) { // backwards
            inventory.SwapItem(-1);
        }

        // Enable getting rid of held weapons
        if(Input.GetKeyDown(KeyCode.T)) {
            inventory.RemoveItem();
        }

        // Chceck if rotation to crosshair pos is necessary
        inventory.RotateItem();

        gun = inventory.heldItem;

        if(Input.GetMouseButtonDown(0)) {
            shooting = true;
        }
        if(Input.GetMouseButtonUp(0)) {
            shooting = false;
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            // Do not reload while reloading to prevent the gun from spawning multiple bullets at once
            if(gun != null && !gun.IsReloading) {
                gun.Reload();
            }
        }

        if(shooting) {
            // Check if shooting is possible
            if(gun != null && gun.CanFire) {
                gun.Shoot();
            }
        }
    }
}
