using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    public Weapon weapon; // The main weapon component (make it public to assign it through the Unity editor)
    public List<GameObject> childComponents; // List of child components such as magazines, scopes, etc.

    void Start()
    {
        // Load the weapon
        LoadWeapon();
    }

    // Load the weapon
    void LoadWeapon()
    {
        // Activate the main weapon component
        weapon.gameObject.SetActive(true);
        // Deactivate all child components
        foreach (var component in childComponents)
        {
            component.SetActive(false);
        }

        // Activate child components with a delay
        StartCoroutine(ActivateComponentsWithDelay());
    }

    // Activate child components with a delay
    IEnumerator ActivateComponentsWithDelay()
    {
        foreach (var component in childComponents)
        {
            yield return new WaitForSeconds(0.2f);  // Delay before activating each component
            component.SetActive(true);
        }
    }
}
