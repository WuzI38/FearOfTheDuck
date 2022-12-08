using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class Player : PersistentSingleton<Player>
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private HealthUI healthUI;
    override protected void Awake()
    {
        base.Awake();
        inventory = gameObject.AddComponent<Inventory>();
    }

    public void AddToPlayerInventory(itemType item) {
        inventory.AddItem(item);
    }

    public void GainHealth(int amount) {
        // Do something
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
    }
}
