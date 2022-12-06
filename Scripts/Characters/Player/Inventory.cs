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
        
        gunList.Add(item);
    }

    public void RemoveItem() {
        gunList.RemoveAt(handIndex);
        SwapItem(-1);
    }

    public void SwapItem(int value) {
        int itemCount = gunList.Count;
        // Swap item only if there are at least to items on the list
        if(itemCount > 1) {
            handIndex = (itemCount + handIndex + value) % itemCount;
        }
        Destroy(handItem);
        InstantiatePrefab(gunList[handIndex]);

    }

    void InstantiatePrefab(itemType item) {
        string path = "Assets/Prefabs/" + item.ToString() + ".prefab";
        handItem = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        Instantiate(handItem, handTransform.position, handTransform.rotation, handTransform);
    }
}

// 
