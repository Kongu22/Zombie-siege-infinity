using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }
    [Header("Ammo")] // Ammo HUD
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;

    public Sprite emptySlot;

    public GameObject crosshair;


    private void Awake() // Singleton
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

    private void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>(); // Get active weapon
        Weapon UnActiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>(); // Get unactive weapon

        if (activeWeapon) // If active weapon is not null
        {
            // Directly display the bullets left in the magazine
            magazineAmmoUI.text = activeWeapon.bulletsLeft.ToString(); // Set magazine ammo
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.thisWeaponModel)}";

            Weapon.WeaponModel model = activeWeapon.thisWeaponModel; // Get active weapon model
            ammoTypeUI.sprite = GetAmmoSprite(model); // Set ammo type sprite

            activeWeaponUI.sprite = GetWeaponSprite(model); // Set active weapon sprite

            if (UnActiveWeapon) // If unactive weapon is not null
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(UnActiveWeapon.thisWeaponModel); // Set unactive weapon sprite
            }
        }
        else // If active weapon is null
        {
            magazineAmmoUI.text = ""; // Set magazine ammo to empty
            totalAmmoUI.text = ""; // Set total ammo to empty

            ammoTypeUI.sprite = emptySlot; // Set ammo type sprite to empty

            activeWeaponUI.sprite = emptySlot; // Set active weapon sprite to empty
            unActiveWeaponUI.sprite = emptySlot; // Set unactive weapon sprite to empty
        }
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol92:
                return (Resources.Load<GameObject>("Pistol92_Weapon")).GetComponent<SpriteRenderer>().sprite; 

            case Weapon.WeaponModel.SAR2000:
                return (Resources.Load<GameObject>("SAR2000_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Scorpion:
                return (Resources.Load<GameObject>("Scorpion_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Glock:
                return (Resources.Load<GameObject>("Glock_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M500:
                return (Resources.Load<GameObject>("M500_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Thompson:
                return (Resources.Load<GameObject>("Thompson_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M16:
                return (Resources.Load<GameObject>("M16_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M4A4:
                return (Resources.Load<GameObject>("M4A4_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.MP40:
                return (Resources.Load<GameObject>("MP40_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.AK47:
                return (Resources.Load<GameObject>("AK47_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.P90:
                return (Resources.Load<GameObject>("P90_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Scar:
                return (Resources.Load<GameObject>("Scar_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.MP7:
                return (Resources.Load<GameObject>("MP7_Weapon")).GetComponent<SpriteRenderer>().sprite;

            default:
                return null;
        }

    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol92:
                return (Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.SAR2000:
                return (Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Scorpion:
                return (Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Glock:
                return (Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M500:
                return (Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Thompson:
                return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M16:
                return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M4A4:
                return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.MP40:
                return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.AK47:
                return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.P90:
                return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Scar:
                return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.MP7:
                return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            default:
                return null;
        }
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        // this will never happen, but we need to return something.
        return null;

    }
}