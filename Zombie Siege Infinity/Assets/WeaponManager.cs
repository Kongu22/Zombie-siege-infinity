// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WeaponManager : MonoBehaviour
// {
//     public static WeaponManager Instance { get;  set; }

//     public List<GameObject> weaponSlots;

//     public GameObject activeWeaponSlot;

//     [Header("Ammo")]
//     public int totalRifleAmmo = 0;
//     public int totalPistolAmmo = 0;
        
    
//     private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(this.gameObject);
//         }
//         else
//         {
//             Instance = this;
//         }
//     }

//     public void Start()
//     {
//         activeWeaponSlot = weaponSlots[0];
//     }

//     public void Update()
//     {
//         foreach(GameObject weaponSlot in weaponSlots)
//         {
//             if(weaponSlot == activeWeaponSlot)
//             {
//                 weaponSlot.SetActive(true);
//             }
//             else
//             {
//                 weaponSlot.SetActive(false);
//             }
//         }

//         if (Input.GetKeyDown(KeyCode.Alpha1))
//         {
//             switchActiveWeaponSlot(0);
//         }
//         if (Input.GetKeyDown(KeyCode.Alpha2))
//         {
//             switchActiveWeaponSlot(1);
//         }
//     }

//     public void PickUpWeapon(GameObject pickedupWeapon)
//     {
//         AddWeaponIntoActiveWeaponSlot(pickedupWeapon);

//     }

//     private void AddWeaponIntoActiveWeaponSlot(GameObject pickedupWeapon)
//     {

//         DropCurrentWeapon(pickedupWeapon);  //Drop the current weapon

//         pickedupWeapon.transform.SetParent(activeWeaponSlot.transform, false ); //Set the parent of the pickedupWeapon to the activeWeaponSlot

//         Weapon weapon = pickedupWeapon.GetComponent<Weapon>(); //Get the Weapon component of the pickedupWeapon

//         pickedupWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z); //Set the local position of the pickedupWeapon to the spawnPosition of the Weapon component
//         pickedupWeapon.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z); //Set the local rotation of the pickedupWeapon to the spawnRotation of the Weapon component
    
//         weapon.isActiveWeapon = true; //Set the isActiveWeapon variable of the Weapon component to true

//         weapon.animator.enabled = true; //Enable the animator of the Weapon component

//         if (pickedupWeapon.GetComponent<Weapon>() != null)
//         {
//             foreach (Transform child in pickedupWeapon.transform)
//             {
//                 child.gameObject.layer = LayerMask.NameToLayer("WeaponRender");
//                 Debug.Log("Weapon active status: " + pickedupWeapon.activeSelf);
//                 Debug.Log("Current layer before change: " + LayerMask.LayerToName(child.gameObject.layer));
//                 child.gameObject.layer = LayerMask.NameToLayer("WeaponRender");
//                 Debug.Log("New layer after change: " + LayerMask.LayerToName(child.gameObject.layer));
//             }
//         }
//     }

//     internal void PickUpAmmo(AmmoBox ammo)
//     {
//         switch (ammo.ammoType)
//         {
//             case AmmoBox.AmmoType.PistolAmmoBox:
//                 totalPistolAmmo += ammo.ammoAmount;
//                 break;
//             case AmmoBox.AmmoType.SmallRifleBox:
//                 totalRifleAmmo += ammo.ammoAmount;
//                 break;
//             case AmmoBox.AmmoType.BigRifleBox:
//                 totalRifleAmmo += ammo.ammoAmount;
//                 break;
//         }
//     }

//     private void DropCurrentWeapon(GameObject pickedupWeapon)
//     {
//         if (activeWeaponSlot.transform.childCount > 0)
//         {
//             var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject; //Get the first child of the activeWeaponSlot

//             weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false; //Set the isActiveWeapon variable of the Weapon component of the weaponToDrop to false
//             weaponToDrop.GetComponent<Weapon>().animator.enabled = false; //Disable the animator of the Weapon component of the weaponToDrop

//             weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent); //Set the parent of the weaponToDrop to null
//             weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition; //Set the local position of the weaponToDrop to the local position of the pickedupWeapon
//             weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation; //Set the local rotation of the weaponToDrop to the local rotation of the pickedupWeapon

            
//                 foreach (Transform child in weaponToDrop.transform)
//                 {
//                     child.gameObject.layer = LayerMask.NameToLayer("Default");
//                     Debug.Log("Changing layer for weapon: " + weaponToDrop.name + " to " + LayerMask.NameToLayer("Default"));
//                 }
//         }
//     }

//     public void switchActiveWeaponSlot(int slotNnumber)
//     {
//         if(activeWeaponSlot.transform.childCount > 0)
//         {
//             Weapon currentWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>(); //Get the Weapon component of the first child of the activeWeaponSlot
//             currentWeapon.isActiveWeapon = false; //Set the isActiveWeapon variable of the currentWeapon to false
//         }

//         activeWeaponSlot = weaponSlots[slotNnumber]; //Set the activeWeaponSlot to the weaponSlot at the slotNumber

//         if(activeWeaponSlot.transform.childCount > 0)
//         {
//             Weapon newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>(); //Get the Weapon component of the first child of the activeWeaponSlot
//             newWeapon.isActiveWeapon = true; //Set the isActiveWeapon variable of the newWeapon to true
//         }
//     }

//     internal void DecreaseTotalAmmo(int bulletsToDecrease, Weapon.WeaponModel thisWeaponModel)
//     {
//             switch (thisWeaponModel)
//         {
//             case Weapon.WeaponModel.Glock:
//                 totalPistolAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.M500:
//                 totalPistolAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.Pistol92:
//                 totalPistolAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.SAR2000:
//                 totalPistolAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.Scorpion:
//                 totalPistolAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.Thompson:
//                 totalRifleAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.M16:
//                 totalRifleAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.M4A4:
//                 totalRifleAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.MP40:
//                 totalRifleAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.AK47:
//                 totalRifleAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.P90:
//                 totalRifleAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.Scar:
//                 totalRifleAmmo -= bulletsToDecrease;
//                 break;
//             case Weapon.WeaponModel.MP7:
//                 totalRifleAmmo -= bulletsToDecrease;
//                 break;

//         }
//     }
//     // Check the ammo left for the weapon model
//     public int CheckAmmoLeftFor(Weapon.WeaponModel thisWeaponModel)
//     {
//         switch (thisWeaponModel)
//         {
//             case Weapon.WeaponModel.Glock:
//                 return totalPistolAmmo;
//             case Weapon.WeaponModel.Pistol92:
//                 return totalPistolAmmo;
//             case Weapon.WeaponModel.SAR2000:
//                 return totalPistolAmmo;
//             case Weapon.WeaponModel.Scorpion:
//                 return totalPistolAmmo;
//             case Weapon.WeaponModel.Thompson:
//                 return totalRifleAmmo;
//             case Weapon.WeaponModel.M16:
//                 return totalRifleAmmo;
//             case Weapon.WeaponModel.M4A4:
//                 return totalRifleAmmo;
//             case Weapon.WeaponModel.MP40:
//                 return totalRifleAmmo;
//             case Weapon.WeaponModel.AK47:
//                 return totalRifleAmmo;
//             case Weapon.WeaponModel.P90:
//                 return totalRifleAmmo;
//             case Weapon.WeaponModel.Scar:
//                 return totalRifleAmmo;
//             case Weapon.WeaponModel.MP7:
//                 return totalRifleAmmo;
//             default:
//                 return 0;
//         }
//     }
// }

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
        AddWeaponIntoActiveWeaponSlot(pickedUpWeapon);
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
