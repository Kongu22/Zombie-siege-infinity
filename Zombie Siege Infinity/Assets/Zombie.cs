using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieHand zombieHand;
    public int zombieDamage;

    public void Start()
    {
        zombieDamage = zombieHand.damage;
    }
}
