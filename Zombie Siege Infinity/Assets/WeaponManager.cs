
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get;  set; }

    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlot;

    [Header("Ammo")]
    public int totalRifleAmmo = 0;
    public int totalPistolAmmo = 0;


    public bool canPickupWeapon = true;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    

    // Start is called before the first frame update
    private void Start()
    {
        activeWeaponSlot = weaponSlots[0];
    }

    // Update is called once per frame
    private void Update()
    {
        CheckActiveWeaponReloading();
        
        foreach (GameObject weaponSlot in weaponSlots)
        {
            weaponSlot.SetActive(weaponSlot == activeWeaponSlot);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveWeaponSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveWeaponSlot(1);
        }
    }

    // Pick up a weapon and add it to the active weapon slot 
    public void PickUpWeapon(GameObject pickedUpWeapon)
    {
        if (canPickupWeapon)
        {
            AddWeaponIntoActiveWeaponSlot(pickedUpWeapon);
        }
    }

        private void CheckActiveWeaponReloading()
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            Weapon activeWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            if (activeWeapon != null)
            {
                canPickupWeapon = !activeWeapon.isReloading;
            }
        }
    }

    // Add the weapon to the active weapon slot 
    private void AddWeaponIntoActiveWeaponSlot(GameObject pickedUpWeapon)
    {
        DropCurrentWeapon(pickedUpWeapon);
        pickedUpWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        pickedUpWeapon.transform.localPosition = Vector3.zero; // Assuming you want it at the center of the slot
        pickedUpWeapon.transform.localRotation = Quaternion.identity;

        Weapon weapon = pickedUpWeapon.GetComponent<Weapon>();
        if (weapon != null)
        {
            weapon.isActiveWeapon = true;
            weapon.animator.enabled = true;
            SetLayerRecursively(pickedUpWeapon, LayerMask.NameToLayer("WeaponRender"));
            weapon.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Drop the current weapon from the active weapon slot 
    private void DropCurrentWeapon(GameObject pickedupWeapon)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            GameObject weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;
            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false;
            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent); //Set the parent of the weaponToDrop to null
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition; //Set the local position of the weaponToDrop to the local position of the pickedupWeapon
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation; //Set the local rotation of the weaponToDrop to the local rotation of the pickedupWeapon

            weaponToDrop.transform.SetParent(null); // Or some other default parent
            SetLayerRecursively(weaponToDrop, LayerMask.NameToLayer("Default"));
            weaponToDrop.GetComponent<BoxCollider>().enabled = true;
        }
    }

    // Set the layer of the weapon and its children
    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    // Switch the active weapon slot 
    public void SwitchActiveWeaponSlot(int slotNumber)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>().isActiveWeapon = false;
        }
        activeWeaponSlot = weaponSlots[slotNumber];
        if (activeWeaponSlot.transform.childCount > 0)
        {
            activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>().isActiveWeapon = true;
        }
    }

    // Pick up ammo and add it to the total ammo count
    internal void PickUpAmmo(AmmoBox ammo)
    {
        switch (ammo.ammoType)
        {
            case AmmoBox.AmmoType.PistolAmmoBox:
                totalPistolAmmo += ammo.ammoAmount;
                break;
            case AmmoBox.AmmoType.SmallRifleBox:
            case AmmoBox.AmmoType.BigRifleBox:
                totalRifleAmmo += ammo.ammoAmount;
                break;
        }
    }

        public void DisableWeapons()
    {
        foreach (GameObject weaponSlot in weaponSlots)
        {
            Weapon weapon = weaponSlot.GetComponentInChildren<Weapon>();
            if (weapon != null)
            {
                weapon.enabled = false; // Assuming the 'Weapon' script controls the shooting
            }
        }
    }

    // Decrease the total ammo count for the weapon model
    internal void DecreaseTotalAmmo(int bulletsToDecrease, Weapon.WeaponModel thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case Weapon.WeaponModel.Glock:
                totalPistolAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.M500:
                totalPistolAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.Pistol92:
                totalPistolAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.SAR2000:
                totalPistolAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.Scorpion:
                totalPistolAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.Thompson:
                totalRifleAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.M16:
                totalRifleAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.M4A4:
                totalRifleAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.MP40:
                totalRifleAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.AK47:
                totalRifleAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.P90:
                totalRifleAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.Scar:
                totalRifleAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.MP7:
                totalRifleAmmo -= bulletsToDecrease;
                break;
        }
    }

    // Check the ammo left for the weapon model
    public int CheckAmmoLeftFor(Weapon.WeaponModel thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case Weapon.WeaponModel.Glock:
                return totalPistolAmmo;
            case Weapon.WeaponModel.Pistol92:
                return totalPistolAmmo;
            case Weapon.WeaponModel.SAR2000:
                return totalPistolAmmo;
            case Weapon.WeaponModel.Scorpion:
                return totalPistolAmmo;
            case Weapon.WeaponModel.Thompson:
                return totalRifleAmmo;
            case Weapon.WeaponModel.M16:
                return totalRifleAmmo;
            case Weapon.WeaponModel.M4A4:
                return totalRifleAmmo;
            case Weapon.WeaponModel.MP40:
                return totalRifleAmmo;
            case Weapon.WeaponModel.AK47:
                return totalRifleAmmo;
            case Weapon.WeaponModel.P90:
                return totalRifleAmmo;
            case Weapon.WeaponModel.Scar:
                return totalRifleAmmo;
            case Weapon.WeaponModel.MP7:
                return totalRifleAmmo;
            default:
                return 0;
        }
    }
}
