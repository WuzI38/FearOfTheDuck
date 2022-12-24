using System.Collections.Generic;
using UnityEngine;

public class Player : PersistentSingleton<Player>
{
    private Inventory inventory;
    private HealthUI healthUI;
    private Gun gun;
    private GameObject gunPrefab;
    private bool shooting;
    private bool isEnabled;
    override protected void Awake()
    {
        base.Awake();
        inventory = gameObject.AddComponent<Inventory>();
        healthUI = gameObject.AddComponent<HealthUI>(); 
        // Set starting health to player's max health
        gun = null;
        shooting = false;
        // isEnabled is used instead of enabled, because I don't want to freeze GetMouseButtonUp/Down methods
        isEnabled = true;
        // Not inside wall is used to check if hand holding a gun is inside a wall, if so shooting is blocked
        // Prevent the layer from shooting during pause
    }

    void Start() {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        healthUI.DisplayStartingHealth();
    }

    void Destroy() {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    public void AddToPlayerInventory(Gun item) {
        inventory.AddItem(item);
    }

    public void GainHealth(int amount) {
        healthUI.GainHealth(amount);
    }
    public void LoseHealth(int amount) {
        healthUI.LoseHealth(amount);
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
            if(gun != null && !gun.IsReloading && isEnabled) {
                gun.Reload();
            }
        }

        if(shooting) {
            // Check if shooting is possible
            if(gun != null && gun.CanFire && isEnabled) {
                gun.Shoot();
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && Objective.Completed) {
            float size = gameObject.GetComponent<SpriteRenderer>().size.x / 2;
            Vector2 exitPos = StartAndExitPicker.ExitPos;
            Vector2 exitPosNew = new Vector2(size + exitPos.x * size * 2, size + exitPos.y * size * 2);
            if(Vector2.Distance(exitPosNew, gameObject.transform.position) <= 1) {
                GameState state = GameManager.Instance.state;
                GameManager.Instance.ChangeState(GameState.Finish);
            }
        }
        
    }

    private void OnGameStateChanged(GameState newGameState) {
        isEnabled = newGameState == GameState.Running;
    }
}
