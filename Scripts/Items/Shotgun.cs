using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public Shotgun(MonoBehaviour player, GunScriptable gunParams) : base(player, gunParams){
        // Set all the parameters (using scriptable object) - inherited from gun
        type = Constants.itemType.Rifle;
    }
    public override void Shoot() {
        // You cannot shoot while reloading or if you have no ammo
        if(!canFire || currentAmmo == 0) return;
        canFire = false;
        currentAmmo -= 1;
        // FindObjectOfType<AudioManager>().Play("Shoot");
        // Here goes bullet instantiation + shooting logic
        // Wait for some amount of time between shots
        player.StartCoroutine(ShootDelay());
    }
}
