using System;
using UnityEngine;

// Todo: Change those to private with setters and getters for encapsulation issues

public abstract class ScriptableBase : ScriptableObject {
    // Basic template for all scriptable objects
    public string Description;
}

public abstract class ScriptableUnitBase : ScriptableBase {
    // Basic template for the player and all of the enemies
    public Type Type;
    public float Health;
    public float Damage;
    public float Speed;
    public float AttackSpeed;
}

public enum Type {
    Hero = 0,
    Duck = 1,
}