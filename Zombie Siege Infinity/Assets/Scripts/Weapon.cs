using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public bool isActiveWeapon; // Indicates if the weapon is active
    public int weaponDamage; // Damage of the weapon

    // Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    // Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    [Header("Shooting Settings")]
    public float spreadIntensity;
    public float spreadIntensityADS;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;
    internal Animator animator;

    // Loading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;
    private PlayerMovement playerMovement;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    bool isADS;

    // Camera settings

    public float normalFOV = 60f; // Normal field of view
    public float pistolZoomFOV = 55f; // Zoomed field of view for pistols
    public float rifleZoomFOV = 50f; // Zoomed field of view for pistols
    public float rifleWithScopeZoomFOV = 45f; // Zoomed field of view for rifles with scope
    // public float rifleZoomFOV = 50f;    // Zoomed field of view for rifles
    private Camera MainCamera;

    public enum WeaponModel
    {
        Pistol92,
        SAR2000,
        Scorpion,
        Glock,
        M500,
        Thompson,
        M16,
        M4A4,
        MP40,
        AK47,
        P90,
        Scar,
        MP7
    }
    public WeaponModel thisWeaponModel; // Current weapon model 

    public enum ShootingMode // Shooting mode object, enum is a type of object that can store a value from a list of values.
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode; // Current shooting mode

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
        playerMovement = GetComponentInParent<PlayerMovement>(); // Assuming the weapon is a child of the player
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveWeapon)
        {
            if (Input.GetMouseButtonDown(1) && !isReloading)
            {
                enterADS(); // Enter Aim Down Sights (ADS) method
            }

            if (Input.GetMouseButtonUp(1))
            {
                exitADS(); // Exit Aim Down Sights (ADS) method
            }

            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instance.emptyMagazineSound.Play();
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading && WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > 0 && !isADS)
            {
                Reload(); // Reload the weapon
            }

            if (isReloading)
            {
                return;
            }

            if (currentShootingMode == ShootingMode.Auto)
            {
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon(); // Fire the weapon
            }
            
            if(isReloading == true)
            {
                //disable the pickupweapon
                WeaponManager.Instance.canPickupWeapon = false;
            }

        }
    }

    // Enter Aim Down Sights (ADS) method
    private void enterADS()
    {
        if (MainCamera == null)
        {
            Debug.LogError("Main camera is not assigned!");
            return;
        }

        // Determine the field of view (FOV) based on the weapon model
        if (thisWeaponModel == WeaponModel.Pistol92 || thisWeaponModel == WeaponModel.SAR2000 ||
            thisWeaponModel == WeaponModel.Scorpion || thisWeaponModel == WeaponModel.Glock)
        {
            MainCamera.fieldOfView = pistolZoomFOV;
        }
        else if (thisWeaponModel == WeaponModel.M4A4 || thisWeaponModel == WeaponModel.Scar ||
                thisWeaponModel == WeaponModel.P90 || thisWeaponModel == WeaponModel.MP7)
        {
            MainCamera.fieldOfView = rifleWithScopeZoomFOV;
        }
        else
        {
            MainCamera.fieldOfView = rifleZoomFOV;
        }

        HUDManager.Instance.crosshair.SetActive(false);
        isADS = true;
        animator.SetTrigger("enterADS");
    }

    // Exit Aim Down Sights (ADS) method
    private void exitADS()
    {
        if (MainCamera == null)
        {
            Debug.LogError("Main camera is not assigned!");
            return;
        }

        // Reset the field of view (FOV) to normal when exiting ADS
        MainCamera.fieldOfView = normalFOV;
        HUDManager.Instance.crosshair.SetActive(true);
        isADS = false;
        animator.SetTrigger("exitADS");
    }

    // Fire weapon method
    private void FireWeapon() // Firing the weapon
    {
        bulletsLeft--; // Decrease the bullets left

        muzzleEffect.GetComponent<ParticleSystem>().Play(); // Play the muzzle effect

        if (isADS)
        {
            animator.SetTrigger("RECOIL_ADS"); // Set the trigger to shoot
        }
        else
        {
            animator.SetTrigger("RECOIL"); // Set the trigger to shoot
        }

        SoundManager.Instance.PlayShootingSound(thisWeaponModel); // Play the shooting sound

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized; // Normalizing the shooting direction, Vector3 is a 3D vector, normalized is a method that returns the vector with a magnitude of 1.

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        Bullet bul = bullet.GetComponent<Bullet>();
        bul.bulletDamage = weaponDamage;

        // Point the bullet in the direction of the shooting.
        bullet.transform.forward = shootingDirection;

        // Shoot the bullet from the blue axis
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse); // AddForce is a method that applies a force to the rigidbody, ForceMode.Impulse is a force mode that applies an instant force to the rigidbody.

        // Destroy the bullet after a certain amount of time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime)); // StartCoroutine is a method that starts a coroutine, DestroyBulletAfterTime is a coroutine that destroys the bullet after a certain amount of time.

        // Check if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) // We already shot once before this
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    // Reload the weapon method
    private void Reload()
    {
        SoundManager.Instance.PlayReloadingSound(thisWeaponModel);

        animator.SetTrigger("RELOAD");

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    // Reload completed method to reset the bullets left
    private void ReloadCompleted()
    {
        int bulletsToLoad = magazineSize - bulletsLeft; // Calculate how many bullets need to be reloaded
        int ammoAvailable = WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel); // Check how much ammo is available

        if (ammoAvailable >= bulletsToLoad)
        {
            bulletsLeft = magazineSize; // Fully reload the magazine if enough ammo
            WeaponManager.Instance.DecreaseTotalAmmo(bulletsToLoad, thisWeaponModel); // Decrease the total ammo by the number of bullets loaded
        }
        else
        {
            bulletsLeft += ammoAvailable; // Only load available ammo
            WeaponManager.Instance.DecreaseTotalAmmo(ammoAvailable, thisWeaponModel); // Decrease the total ammo by the actual amount loaded
        }

        isReloading = false; // Mark reloading as complete
    }

    // Reset the shot after a certain amount of time
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    // Calculate the direction and spread of the shooting
    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // ViewportPointToRay is a method that returns a ray going from the camera through a viewport point.
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            // Hitting something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting at the air.
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position; // Calculating the direction of the shooting.

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity); // Random.Range is a method that returns a random float number between the min and max values.
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity); // Random.Range is a method that returns a random float number between the min and max values.
        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity); // Random.Range is a method that returns a random float number between the min and max values.

        if (isADS)
        {
            x = UnityEngine.Random.Range(-spreadIntensityADS, spreadIntensityADS); // Random.Range is a method that returns a random float number between the min and max values.
            y = UnityEngine.Random.Range(-spreadIntensityADS, spreadIntensityADS); // Random.Range is a method that returns a random float number between the min and max values.
            z = UnityEngine.Random.Range(-spreadIntensityADS, spreadIntensityADS); // Random.Range is a method that returns a random float number between the min and max values.
        }

        return direction + new Vector3(x, y, z); // Returning the direction of the shooting plus the spread.
    }

    // Destroy the bullet after a certain amount of time
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
