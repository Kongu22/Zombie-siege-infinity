using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get;  set; }

    public List<GameObject> weaponSlots;

    public GameObject activeWeaponSlot;

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

    public void Start()
    {
        activeWeaponSlot = weaponSlots[0];
    }

    public void Update()
    {
        foreach(GameObject weaponSlot in weaponSlots)
        {
            if(weaponSlot == activeWeaponSlot)
            {
                weaponSlot.SetActive(true);
            }
            else
            {
                weaponSlot.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchActiveWeaponSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchActiveWeaponSlot(1);
        }
    }

    public void PickUpWeapon(GameObject pickedupWeapon)
    {
        AddWeaponIntoActiveWeaponSlot(pickedupWeapon);

    }

    private void AddWeaponIntoActiveWeaponSlot(GameObject pickedupWeapon)
    {

        DropCurrentWeapon(pickedupWeapon);  //Drop the current weapon

        pickedupWeapon.transform.SetParent(activeWeaponSlot.transform, false ); //Set the parent of the pickedupWeapon to the activeWeaponSlot

        Weapon weapon = pickedupWeapon.GetComponent<Weapon>(); //Get the Weapon component of the pickedupWeapon

        pickedupWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z); //Set the local position of the pickedupWeapon to the spawnPosition of the Weapon component
        pickedupWeapon.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z); //Set the local rotation of the pickedupWeapon to the spawnRotation of the Weapon component
    
        weapon.isActiveWeapon = true; //Set the isActiveWeapon variable of the Weapon component to true

        weapon.animator.enabled = true; //Enable the animator of the Weapon component
    }
    
    private void DropCurrentWeapon(GameObject pickedupWeapon)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject; //Get the first child of the activeWeaponSlot

            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false; //Set the isActiveWeapon variable of the Weapon component of the weaponToDrop to false
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false; //Disable the animator of the Weapon component of the weaponToDrop

            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent); //Set the parent of the weaponToDrop to null
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition; //Set the local position of the weaponToDrop to the local position of the pickedupWeapon
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation; //Set the local rotation of the weaponToDrop to the local rotation of the pickedupWeapon
        }
    }

    public void switchActiveWeaponSlot(int slotNnumber)
    {
        if(activeWeaponSlot.transform.childCount > 0)
        {
            Weapon currentWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>(); //Get the Weapon component of the first child of the activeWeaponSlot
            currentWeapon.isActiveWeapon = false; //Set the isActiveWeapon variable of the currentWeapon to false
        }

        activeWeaponSlot = weaponSlots[slotNnumber]; //Set the activeWeaponSlot to the weaponSlot at the slotNumber

        if(activeWeaponSlot.transform.childCount > 0)
        {
            Weapon newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>(); //Get the Weapon component of the first child of the activeWeaponSlot
            newWeapon.isActiveWeapon = true; //Set the isActiveWeapon variable of the newWeapon to true
        }
    }
}

