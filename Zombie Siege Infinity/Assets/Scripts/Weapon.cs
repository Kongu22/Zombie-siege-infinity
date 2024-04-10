using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{

    public bool isActiveWeapon; // Is the weapon active

    //Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //Spread
    public float spreadIntensity;

    //Bullet
   public GameObject bulletPrefab;
   public Transform bulletSpawn;
   public float bulletVelocity = 30;
   public float bulletPrefabLifeTime = 3f;

   public GameObject muzzleEffect;
   internal Animator animator; 

   //Loading
   public float reloadTime;
   public int magazineSize, bulletsLeft;
   public bool isReloading;
   private PlayerMovement playerMovement;

   public Vector3 spawnPosition;
   public Vector3 spawnRotation;


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
    }



    // Update is called once per frame
    void Update()
    {
        if (isActiveWeapon)
        { 
            if(bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instance.emptyMagazineSound.Play();
            }

            // Check if the reload button is pressed and if reload conditions are met
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading)
            {
                Reload();
            }

            // If the weapon is reloading, ignore shooting inputs
            if (isReloading)
            {
                return; // Exit the Update method early if reloading
            }

            // Determine shooting based on shooting mode and button presses
            if (currentShootingMode == ShootingMode.Auto)
            {
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            // Proceed with shooting if ready and button is pressed
            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }


        }
        
    }

    private void FireWeapon() // Firing the weapon
    {
        bulletsLeft--; // Decrease the bullets left

        muzzleEffect.GetComponent<ParticleSystem>().Play(); // Play the muzzle effect
        animator.SetTrigger("RECOIL"); // Set the trigger to shoot


        SoundManager.Instance.PlayShootingSound(thisWeaponModel); // Play the shooting sound


        readyToShoot = false;

        // Vector3 shootingDirection = CalculateDirectionAndSpread().normalized; // Normalizing the shooting direction, Vector3 is a 3D vector, normalized is a method that returns the vector with a magnitude of 1.
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Pointing the bullet to the direction of the shooting.
        bullet.transform.forward = shootingDirection;

        //Shoot the bullet from the blue axis
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse); // AddForce is a method that applies a force to the rigidbody, ForceMode.Impulse is a force mode that applies an instant force to the rigidbody.

        //Destroy the bullet after a certain amount of time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime)); // StartCoroutine is a method that starts a coroutine, DestroyBulletAfterTime is a coroutine that destroys the bullet after a certain amount of time.

        //Checking if we are done shooting
        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //Burst Mode
        if(currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) // we already shot once before this
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {
        SoundManager.Instance.PlayReloadingSound(thisWeaponModel);

        animator.SetTrigger("RELOAD");

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    
    }

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


        return direction + new Vector3(x, y, z); // Returning the direction of the shooting plus the spread.

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
