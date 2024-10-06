using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieHand zombieHand;
    public int zombieDamage;

    // This method is called when the Zombie object is created
    public void Start()
    {
        // Assign the damage value from the ZombieHand object to the zombieDamage variable
        zombieDamage = zombieHand.damage;
    }
}
