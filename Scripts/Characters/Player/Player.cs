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
        inventory = new Inventory();
    }

    public void AddToPlayerInventory(itemType item) {
        inventory.AddItem(item);
    }

    public void GainHealth(int amount) {
        // Do something
    }

    void Update()
    {
        
    }
}
