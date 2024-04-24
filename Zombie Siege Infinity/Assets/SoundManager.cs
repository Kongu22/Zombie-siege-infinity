using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get;  set; }

    public AudioSource ShootingChannel;

    public AudioSource emptyMagazineSound;

    [Header(" Shooting Sounds")]
    public AudioClip Pistol92Shot;
    public AudioClip SAR2000Shot;
    public AudioClip GlockShot;
    public AudioClip ThompsonShot;
    public AudioClip MP40Shot;
    public AudioClip M4A4Shot;
    public AudioClip AK47Shot;
    public AudioClip P90Shot;
    public AudioClip ScarShot;
    public AudioClip MP7Shot;
    public AudioClip Scorpion;

    [Header(" Reloading Sounds")]
    public AudioSource ReloadingSoundPistol92;
    public AudioSource ReloadingSoundScorpion;
    public AudioSource ReloadingSoundSAR2000;
    public AudioSource ReloadingSoundGlock;
    public AudioSource ReloadingSoundThompson;
    public AudioSource ReloadingSoundMP40;
    public AudioSource ReloadingSoundM4A4;
    public AudioSource ReloadingSoundAK47;
    public AudioSource ReloadingSoundP90;
    public AudioSource ReloadingSoundScar;
    public AudioSource ReloadingSoundMP7;

    [Header("Zombie Sound")]
    public AudioSource ZombieChannel;
    public AudioClip ZombieWalking;
    public AudioClip ZombieChase;
    public AudioClip ZombieAttack;
    public AudioClip ZombieHit;
    public AudioClip ZombieDeath;

    

    [Header("Player Sound")]
    public AudioSource PlayerChannel;
    public AudioClip PlayerHit;
    public AudioClip PlayerDeath;
    public AudioClip DeathMusic;


    [Header("Heli Sound")]
    public AudioSource HeliChannel;

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

    // Plays the shooting sound based on the weapon model
    public void PlayShootingSound(Weapon.WeaponModel weaponModel)
    {
        switch(weaponModel)
        {
            case Weapon.WeaponModel.Pistol92:
                ShootingChannel.PlayOneShot(Pistol92Shot);
                break;
            case Weapon.WeaponModel.SAR2000:
                ShootingChannel.PlayOneShot(SAR2000Shot);
                break;
            case Weapon.WeaponModel.Scorpion:
                ShootingChannel.PlayOneShot(SAR2000Shot);
                break;
            case Weapon.WeaponModel.Glock:
                ShootingChannel.PlayOneShot(GlockShot);
                break;
            case Weapon.WeaponModel.M500:
                // No sound defined for M500
                break;
            case Weapon.WeaponModel.Thompson:
                ShootingChannel.PlayOneShot(ThompsonShot);
                break;
            case Weapon.WeaponModel.M16:  
                ShootingChannel.PlayOneShot(M4A4Shot);
                break;
            case Weapon.WeaponModel.M4A4:
                ShootingChannel.PlayOneShot(M4A4Shot);
                break;
            case Weapon.WeaponModel.MP40:
                ShootingChannel.PlayOneShot(MP40Shot);
                break;
            case Weapon.WeaponModel.AK47:
                ShootingChannel.PlayOneShot(AK47Shot);
                break;
            case Weapon.WeaponModel.P90:
                ShootingChannel.PlayOneShot(P90Shot);
                break;
            case Weapon.WeaponModel.Scar:
                ShootingChannel.PlayOneShot(ScarShot);
                break;
            case Weapon.WeaponModel.MP7:
                ShootingChannel.PlayOneShot(MP7Shot);
                break;
        }
    }

    // Plays the reloading sound based on the weapon model
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
                // No sound defined for M500
                break;
            case Weapon.WeaponModel.Thompson:
                ReloadingSoundThompson.Play();
                break;
            case Weapon.WeaponModel.M16:  
                ReloadingSoundM4A4.Play();
                break;
            case Weapon.WeaponModel.M4A4:
                ReloadingSoundM4A4.Play();
                break;
            case Weapon.WeaponModel.MP40:
                ReloadingSoundMP40.Play();
                break;
            case Weapon.WeaponModel.AK47:
                ReloadingSoundAK47.Play();
                break;
            case Weapon.WeaponModel.P90:
                ReloadingSoundP90.Play();
                break;
            case Weapon.WeaponModel.Scar:
                ReloadingSoundScar.Play();
                break;
            case Weapon.WeaponModel.MP7:
                ReloadingSoundMP7.Play();
                break;
        }
    }
}
