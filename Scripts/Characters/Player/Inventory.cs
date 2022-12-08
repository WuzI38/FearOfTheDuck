using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEditor;

public class Inventory : PersistentSingleton<Inventory>
{
    private static List<itemType> gunList;
    private int handIndex;
    private GameObject handItem;
    private Transform handTransform;

    protected override void Awake() { 
        base.Awake();
        gunList = new List<itemType>();
        handIndex = 0;
        handItem = null;
        handTransform = GameObject.FindGameObjectWithTag("Hand").transform;
    }

    public void AddItem(itemType item) {
        // Do not add an item if it is already on the list
        if(gunList.Contains(item)) return;
        
        // Add item to player's hand if item list is empty
        if(gunList.Count == 0) {
            InstantiatePrefab(item);
        }
        gunList.Add(item);
    }

    public void RemoveItem() {
        // This feature has to be fixed or totally removed

        // Do not get rid of the item if there is no items in hand
        if(gunList.Count == 0) return;
        // Last item in hand is destroyed
        if(gunList.Count == 1) {
            // If there is only one gun just remove it (no swaps)
            gunList.RemoveAt(0);
            Helpers.Utils.DestroyChildren(handTransform);
            handItem = null;
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

    void InstantiatePrefab(itemType item) {
        string path = "Assets/Prefabs/" + item.ToString() + ".prefab";
        handItem = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        Instantiate(handItem, handTransform.position, handTransform.rotation, handTransform);
    }

    public void RotateItem() {
        // Rotate hand so it points to crosshair's position
        Vector2 crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 handPos = handTransform.position;
        int sign = (crosshairPos.y > handPos.y) ? 1 : -1;

        float angle = Vector2.Angle(handPos, crosshairPos) * sign;
        // float newAngle = angle - handTransform.rotation.z;

        // THIS MUST BE FIXED
        // Try rotating hand, so it points towards the crosshair
        if(handItem != null) {
            handTransform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}


