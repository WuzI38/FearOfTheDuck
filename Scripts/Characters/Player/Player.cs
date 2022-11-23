using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private HealthUI healthUI;
    // Player derives after singleton, cause there may be only one
    override protected void Awake()
    {
        base.Awake();
        inventory = new Inventory();
    }

    void Update()
    {
        
    }
}
