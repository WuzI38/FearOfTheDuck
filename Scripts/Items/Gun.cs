using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

class Gun : Item {
    protected float damage; // Amount of damage dealt
    protected float spread; // Max angle between anticipated shooting direction and real shottting direction
    protected float reloadTime; // Amount of time to relaod a gun
    protected float bulletSpeed; // Speed of bullets the current gun is firing
    protected float delay; // Delay between shots
    protected bool canFire; // False if firing or reloading, true otherwise
    protected int maxAmmo;
    protected int currentAmmo;

    protected virtual void Reload() {
        canFire = false;
        currentAmmo = maxAmmo;
        // FindObjectOfType<AudioManager>().Play("Reload");
        // Block shooting for some time necessary for the player to reload
        StartCoroutine(ReloadDelay());
    }
    protected virtual void Shoot() {
        // You cannot shoot while reloading or if you have no ammo
        if(!canFire || currentAmmo == 0) return;
        canFire = false;
        currentAmmo -= 1;
        // FindObjectOfType<AudioManager>().Play("Shoot");
        // Here goes bullet instantiation + shooting logic
        // Wait for some amount of time between shots
        StartCoroutine(ShootDelay());
    }

    protected IEnumerator ShootDelay() {
        yield return new WaitForSeconds(delay);
        canFire = true;
    }

    protected IEnumerator ReloadDelay() {
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
    }
}