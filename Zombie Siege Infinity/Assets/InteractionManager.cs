using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;
    private float interactionDistance = 5f; // Distance for interaction

    private void Awake()
    {
        // Ensure only one instance of InteractionManager exists
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
        // Create a ray from the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Draw the ray in the scene view for debugging purposes
        Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance)) // Check for collision within interactionDistance
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;
            float distanceToObject = Vector3.Distance(Camera.main.transform.position, objectHitByRaycast.transform.position); // Calculate distance to object

            
            // Weapon interaction
            if (objectHitByRaycast.GetComponent<Weapon>()) // Check if it's a weapon within range
            {
                if (hoveredWeapon != null)
                    hoveredWeapon.GetComponent<Outline>().enabled = false;

                hoveredWeapon = objectHitByRaycast.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        WeaponManager.Instance.PickUpWeapon(objectHitByRaycast.gameObject);
                        hoveredWeapon.GetComponent<Outline>().enabled = false;
                        print("The player picked up a weapon! " + objectHitByRaycast.name);
                        hoveredWeapon = null;
                    }
                
                    
            }
            else
            {
                if (hoveredWeapon != null)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                    hoveredWeapon = null;
                }
            }

            // Ammo interaction
            if (objectHitByRaycast.GetComponent<AmmoBox>()) // Check if it's an ammo box within range
            {
                if (hoveredAmmoBox != null)
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;

                hoveredAmmoBox = objectHitByRaycast.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

            }
            else
            {
                if (hoveredAmmoBox != null)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    hoveredAmmoBox = null;
                }
            }
        }
        else
        {
            // Reset hovered objects if no collision
            if (hoveredWeapon != null)
            {
                hoveredWeapon.GetComponent<Outline>().enabled = false;
                hoveredWeapon = null;
            }
            if (hoveredAmmoBox != null)
            {
                hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                hoveredAmmoBox = null;
            }
        }
        
    }
    
}
