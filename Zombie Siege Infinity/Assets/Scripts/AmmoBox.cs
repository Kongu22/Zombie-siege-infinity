using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 200;
    public AmmoType ammoType;

    // Enum to represent different types of ammo boxes
    public enum AmmoType
    {
        PistolAmmoBox,   // Ammo box for pistols
        SmallRifleBox,  // Ammo box for small rifles
        BigRifleBox     // Ammo box for big rifles
    }
}
