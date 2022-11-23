using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Sriptables/Block")]
public class Block : ScriptableBase {
    public Id Id;
}

[Serializable]
public enum Id {
    StoneWall = 0,
    StoneFloor = 1,
    WoodenDoor = 2,
}