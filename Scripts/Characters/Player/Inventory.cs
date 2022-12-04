//using System.Collections;
using System.Collections.Generic;
using Constants;
//using UnityEngine;

public class Inventory // Cannot be a singleton, because of monobehaviour instance creation error
{
    private static List<Item> itemList;
    private int handIndex;

    public Inventory() { 
        itemList = new List<Item>();
        handIndex = 0;
    }

    public void AddItem(itemType item) {
        // Check if the item is already on the list
        bool onList = false;
        foreach(Item itemL in itemList) {
            if(itemL.Type == item) {
                onList = true;
                break;
            }
        }
        // Do not add an item if an item of that type is already on the list
        if(onList) return;
    }

    public void RemoveItem() {
        itemList.RemoveAt(handIndex);
        SwapItem(-1);
    }

    public void SwapItem(int value) {
        int itemCount = itemList.Count;
        // Swap item only if there are at least to items on the list
        if(itemCount > 1) {
            handIndex = (itemCount + handIndex + value) % itemCount;
        }
    }
}
