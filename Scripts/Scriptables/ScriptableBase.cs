using UnityEngine;

// Todo: Change those to private with setters and getters for encapsulation issues

public abstract class ScriptableBase : ScriptableObject {
    // Basic template for all scriptable objects
    public string Description;
}

public abstract class ScriptableUnitBase : ScriptableBase {
    // Basic template for the all the enemies
    public int health;
    public int damage;
    public float speed;
    public float attackSpeed;
}