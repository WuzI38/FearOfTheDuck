using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Sriptables/Gun")]
public class GunScriptable : ScriptableBase {
    public float damage; // Amount of damage dealt
    public float spread; // Max angle between anticipated shooting direction and real shottting direction
    public float reloadTime; // Amount of time to relaod a gun
    public float bulletSpeed; // Speed of bullets the current gun is firing
    public float delay; // Delay between shots
    public int maxAmmo;
}