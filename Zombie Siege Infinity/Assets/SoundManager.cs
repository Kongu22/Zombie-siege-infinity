using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get;  set; }

    public AudioSource emptyMagazineSound;
    public AudioSource shootingSoundPistol92;
    public AudioSource ReloadingSoundPistol92;
    public AudioSource ReloadingSoundScorpion;
    public AudioSource shootingSoundSAR2000;
    public AudioSource ReloadingSoundSAR2000;
    public AudioSource shootingSoundGlock;
    public AudioSource ReloadingSoundGlock;
    public AudioSource shootingSoundM4A4;
    public AudioSource ReloadingSoundM4A4;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(Weapon.WeaponModel weaponModel)
    {
        switch(weaponModel)
        {
            case Weapon.WeaponModel.Pistol92:
                shootingSoundPistol92.Play();
                break;
            case Weapon.WeaponModel.SAR2000:
                shootingSoundSAR2000.Play();
                break;
            case Weapon.WeaponModel.Scorpion:
                break;
            case Weapon.WeaponModel.Glock:
                shootingSoundGlock.Play();
                break;
            case Weapon.WeaponModel.M500:
                break;
            case Weapon.WeaponModel.Thompson:
                break;
            case Weapon.WeaponModel.M16:  
                shootingSoundM4A4.Play();
                break;
            case Weapon.WeaponModel.M4A4:
                shootingSoundM4A4.Play();
                break;
            case Weapon.WeaponModel.MP40:
                break;
            case Weapon.WeaponModel.AK47:
                break;
            case Weapon.WeaponModel.P90:
                break;
            case Weapon.WeaponModel.Scar:
                break;
            case Weapon.WeaponModel.MP7:
                break;
        }
    }



    public void PlayReloadingSound(Weapon.WeaponModel weaponModel)
    {
        switch(weaponModel)
        {
            case Weapon.WeaponModel.Pistol92:
                ReloadingSoundPistol92.Play();
                break;
            case Weapon.WeaponModel.SAR2000:
                ReloadingSoundSAR2000.Play();
                break;
            case Weapon.WeaponModel.Scorpion:
                ReloadingSoundScorpion.Play();
                break;
            case Weapon.WeaponModel.Glock:
                ReloadingSoundGlock.Play();
                break;
            case Weapon.WeaponModel.M500:
                break;
            case Weapon.WeaponModel.Thompson:
                break;
            case Weapon.WeaponModel.M16:  
                ReloadingSoundM4A4.Play();
                break;
            case Weapon.WeaponModel.M4A4:
                ReloadingSoundM4A4.Play();
                break;
            case Weapon.WeaponModel.MP40:
                break;
            case Weapon.WeaponModel.AK47:
                break;
            case Weapon.WeaponModel.P90:
                break;
            case Weapon.WeaponModel.Scar:
                break;
            case Weapon.WeaponModel.MP7:
                break;
        }
    }
}
