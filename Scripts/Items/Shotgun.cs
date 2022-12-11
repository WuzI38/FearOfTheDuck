using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public Shotgun(MonoBehaviour player, GunScriptable gunParams) : base(player, gunParams){
        // Set all the parameters (using scriptable object) - inherited from gun
        type = Constants.itemType.Shotgun;
    }
    public override void Shoot() {
        // You cannot shoot while reloading or if you have no ammo
        if(!canFire) return;
        if(currentAmmo == 0) {
            Reload();
            return;
        }
        canFire = false;
        currentAmmo -= 1;
        // FindObjectOfType<AudioManager>().Play("Shoot");

        // Wait for some amount of time given in the gunParams object
        for(float x = -(spread*2); x <= (spread*2); x += spread) {
            InstantiateBullet(x);
        }
        
        player.StartCoroutine(ShootDelay());
    }
}
