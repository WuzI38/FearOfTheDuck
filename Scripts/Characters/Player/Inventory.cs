using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Inventory : PersistentSingleton<Inventory>
{
    private static List<Gun> gunList;
    private int handIndex;
    private Gun handItem;
    public Gun heldItem {
        get => handItem;
    }
    private GameObject handItemPrefab;
    public GameObject heldItemPrefab {
        get => handItemPrefab;
    }
    private Transform handTransform;
    private GameObject crosshair;
    private bool isEnabled;

    protected override void Awake() { 
        base.Awake();
        gunList = new List<Gun>();
        handIndex = 0;
        handItem = null;
        handItemPrefab = null;
        handTransform = GameObject.FindGameObjectWithTag("Hand").transform;
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        isEnabled = true;
        // Enable pausing the inventory
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void Destroy() {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    public void AddItem(Gun item) {
        // Do not add an item if it is already on the list
        if(gunList.Contains(item)) return;
        
        // Add item to player's hand if item list is empty
        if(gunList.Count == 0) {
            InstantiatePrefab(item);
        }
        gunList.Add(item);
    }

    public void RemoveItem() {
        // If the game is paused DO NOT
        if(!isEnabled) return;
        // Do not get rid of the item if there is no items in hand
        if(gunList.Count == 0) return;
        // Last item in hand is destroyed
        if(gunList.Count == 1) {
            // If there is only one gun just remove it (no swaps)
            gunList.RemoveAt(0);
            Helpers.Utils.DestroyChildren(handTransform);
            handItem = null;
            handItemPrefab = null;
        }
        else {
            int index = handIndex;
            SwapItem(-1);
            // Check if new hand item is the last one on the list, if so adjust idices
            if(handIndex == gunList.Count - 1) handIndex--;
            gunList.RemoveAt(index);
        }
    }

    public void SwapItem(int value) {
        // If the game is paused DO NOT
        if(!isEnabled) return;
        // Do not swap items if there is no item in hand
        if(gunList.Count == 0) return;
        int itemCount = gunList.Count;
        // Swap item only if there are at least to items on the list
        if(itemCount > 1) {
            // DO NOT DIVIDE BY 0
            handIndex = (itemCount + handIndex + value) % Mathf.Max(itemCount, 0);
        }
        // Destroy all items which were placed in hand before
        Helpers.Utils.DestroyChildren(handTransform);
        InstantiatePrefab(gunList[handIndex]);
    }

    void InstantiatePrefab(Gun item) {
        // Create prefab from the prefab folder
        string path = "Assets/Prefabs/" + item.Type.ToString() + ".prefab";
        handItemPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        handItem = item;
        float offset = handItemPrefab.transform.localScale.z * 0.5f;
        handItemPrefab = GameObject.Instantiate(handItemPrefab, handTransform.position, handTransform.rotation, handTransform) as GameObject;
    }

    public void RotateItem() {
        // If the game is paused DO NOT
        if(!isEnabled) return;
        // This may not be perfect but... that is enough
        Vector2 crosshairPos = crosshair.transform.position; 
        Vector2 handPos = handTransform.position;
        // Minimize the gap between hand and weapon
        handPos.y -= crosshair.GetComponent<Crosshair>().spriteY * 0.16f;

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        // Calculate angle needed to rotate player's hand around his body
        float angle = Mathf.Atan2(crosshairPos.y - player.position.y, crosshairPos.x - player.position.x) * 180 / Mathf.PI;
        float handAngle = Mathf.Atan2(handPos.y - player.position.y, handPos.x - player.position.x) * 180 / Mathf.PI;
        float diff = angle - handAngle;

        /*Debug.Log("Cross: " + angle);
        Debug.Log("hand: " + handAngle);*/
        
        // Rotate player's hand
        if(handItemPrefab != null) {
            handTransform.RotateAround(player.position, Vector3.forward, diff);
        }
    }

    public List<string> AsString() {
        // Return every inventory item as string
        if(gunList.Count == 0) return null;
        List<string> names = new List<string>();
        foreach(Gun gun in gunList) {
            Constants.itemType type = gun.Type;
            names.Add(type.ToString());
        }
        return names;
    }
    private void OnGameStateChanged(GameState newGameState) {
        enabled = newGameState == GameState.Running;
        isEnabled = newGameState == GameState.Running;
    }
}