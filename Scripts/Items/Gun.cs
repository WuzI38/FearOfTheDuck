using System.Collections;
using UnityEngine;
using UnityEditor;

// Abstract gun class for gun creation
public abstract class Gun : Item {
    protected float damage; // Amount of damage dealt
    protected float spread; // Max angle between anticipated shooting direction and real shottting direction
    protected float reloadTime; // Amount of time to relaod a gun
    protected float bulletSpeed; // Speed of bullets the current gun is firing
    protected float delay; // Delay between shots
    protected bool canFire; // False if firing or reloading, true otherwise
    public bool CanFire {
        get => canFire;
    }
    protected int maxAmmo;
    protected int currentAmmo;
    protected MonoBehaviour player;
    protected GunScriptable gunParams;
    protected bool isReloading;
    public bool IsReloading {
        get => isReloading;
    }

    public Gun(MonoBehaviour player, GunScriptable gunParams) {
        damage = gunParams.damage;
        spread = gunParams.spread;
        reloadTime = gunParams.reloadTime;
        bulletSpeed = gunParams.bulletSpeed;
        delay = gunParams.delay;
        maxAmmo = gunParams.maxAmmo;
        currentAmmo = maxAmmo;
        canFire = true;
        this.player = player;
        this.gunParams = gunParams;
        this.isReloading = false;
    }

    public virtual void Reload() {
        canFire = false;
        isReloading = true;
        currentAmmo = maxAmmo;
        // FindObjectOfType<AudioManager>().Play("Reload");
        // Block shooting for some time necessary for the player to reload
        player.StartCoroutine(ReloadDelay());
    }
    public virtual void Shoot() {
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
        InstantiateBullet();
        
        player.StartCoroutine(ShootDelay());
    }

    protected void InstantiateBullet(float extraRotation = 0) {
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

        int mult = 1;
        // Check if crosshair is inside the player, if so multiply direction by -1
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if(Vector2.Distance(crosshairPos, playerPos) < Vector2.Distance(muzzle.position, playerPos)) mult = -1;

        // Find bullet's movement vector
        Vector2 direction = (crosshairPos - new Vector2(muzzle.position.x, muzzle.position.y)).normalized;
        //direction -= playerSpeed.normalized;
        direction = direction * bulletSpeed * Time.fixedDeltaTime * mult;

        // Apply weapon's spread
        if(spread > 0) {
            float rotation = Random.Range(-spread / 2, spread / 2);
            direction = Quaternion.AngleAxis(rotation, Vector3.forward) * direction;
        }

        direction = Quaternion.AngleAxis(extraRotation, Vector3.forward) * direction;

        // Custom method for button init must be created, as it is a MonoBehaviour, so it can't have constructor
        bulletScript.SetParams(direction, damage);
    }

    protected IEnumerator ShootDelay() {
        yield return new WaitForSeconds(delay);
        // Do not shoot if you're reloading
        if(!isReloading) canFire = true;
    }

    protected IEnumerator ReloadDelay() {
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
        isReloading = false;
    }
}