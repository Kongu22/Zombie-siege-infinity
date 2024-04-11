﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;

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

    public void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        
        //Weapon raycast interaction

        if(Physics.Raycast(ray, out hit))  //If the raycast hits something
        {
            GameObject objectHitByRaycast = hit.transform.gameObject; //This is the object that the raycast hit

            if(objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false) //If the object that the raycast hit has a Weapon component
            {
                hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>(); //Set the hoveredWeapon to the Weapon component of the object that the raycast hit
                hoveredWeapon.GetComponent<Outline>().enabled = true; //Enable the outline of the hoveredWeapon

                if (Input.GetKeyDown(KeyCode.F)) //If the player presses the F key
                {
                    WeaponManager.Instance.PickUpWeapon(objectHitByRaycast.gameObject); //Call the PickUpWeapon function from the WeaponManager script
                    hoveredWeapon.GetComponent<Outline>().enabled = false; //Disable the outline of the hoveredWeapon
                    print("the player picked up a weapon!" + objectHitByRaycast.gameObject); //Print a message to the console
                    
                }
            }
            else
            {
                if(hoveredWeapon) //If there is a hoveredWeapon
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false; //Disable the outline of the hoveredWeapon
                }
            }

            //Ammo raycast interaction
            if (objectHitByRaycast.GetComponent<AmmoBox>())  //If the object that the raycast hit has a Ammo component
            {
                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>(); //Set the hoveredAmmoBox to the Ammo component of the object that the raycast hit
                hoveredAmmoBox.GetComponent<Outline>().enabled = true; //Enable the outline of the hoveredAmmoBox

                if (Input.GetKeyDown(KeyCode.F)) //If the player presses the F key
                {
                    WeaponManager.Instance.PickUpAmmo(hoveredAmmoBox); //Call the PickUpAmmo function from the WeaponManager script
                    Destroy(objectHitByRaycast.gameObject); //Destroy the hoveredAmmoBox

                }
            }
            else
            {
                if (hoveredAmmoBox) //If there is a hoveredAmmoBox
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false; //Disable the outline of the hoveredAmmoBox
                }
            }   

        }
    }

}

