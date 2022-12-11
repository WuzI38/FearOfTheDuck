using UnityEngine;
using Constants;

// This script is really short, as all the items currently implemented are guns

public abstract class Item
{
    protected itemType type;
    public itemType Type {
        get => type;
    }
}

