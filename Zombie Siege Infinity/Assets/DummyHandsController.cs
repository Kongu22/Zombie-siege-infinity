using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHandsController : MonoBehaviour
{
    public Weapon weapon; // Ссылка на скрипт оружия
    public GameObject leftHand; // Левая рука-манекен
    public GameObject rightHand; // Правая рука-манекен

    void Update()
    {
        if (weapon.isActiveWeapon)
        {
            // Активируем руки-манекена, если оружие активно
            leftHand.SetActive(true);
            rightHand.SetActive(true);
        }
        else
        {
            // Деактивируем руки-манекена, если оружие не активно
            leftHand.SetActive(false);
            rightHand.SetActive(false);
        }
    }
}
