using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rifle : Gun
{
    public Rifle(MonoBehaviour player, GunScriptable gunParams) : base(player, gunParams){
        // Set all the parameters (using scriptable object) - inherited from gun
        type = Constants.itemType.Rifle;
    }
    public override void Shoot() {
        // You cannot shoot while reloading or if you have no ammo
        if(!canFire || currentAmmo == 0) return;
        canFire = false;
        currentAmmo -= 1;
        // FindObjectOfType<AudioManager>().Play("Shoot");

        // Shoot only if you can find the muzzle and get its position
        Transform muzzle = null;
        muzzle = GameObject.FindGameObjectWithTag("Muzzle").transform;
        if(muzzle == null) return;

        // Initialize item on the given path
        string path = "Assets/Prefabs/Bullet.prefab";
        GameObject bullet = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        bullet = GameObject.Instantiate(bullet, muzzle.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Find destination (crosshair's position), then normalize the vector
        Vector2 crosshairPos = GameObject.FindGameObjectWithTag("Crosshair").transform.position;

        // Get player's velocity (I don't know, maybe calculating positions will be easier thanks to that)
        Vector2 playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;

        // Find bullet's movement vector
        Vector2 direction = (crosshairPos - new Vector2(muzzle.position.x, muzzle.position.y)).normalized;
        //direction -= playerSpeed.normalized;
        direction = direction * bulletSpeed * Time.deltaTime;

        // Custom method for button init must be created, as it is a MonoBehaviour, so it can't have constructor
        bulletScript.SetParams(direction, damage);

        // Wait for some amount of time given in the gunParams object
        player.StartCoroutine(ShootDelay());
    }
}
