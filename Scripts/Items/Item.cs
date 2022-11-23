using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    protected enum ItemType {
        Health,
        Rifle,
        Pistol
    }
    protected ItemType type;
}
