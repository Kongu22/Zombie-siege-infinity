using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    // Singleton instance of GlobalReferences
    public static GlobalReferences Instance { get; set; }

    // Prefab for bullet impact effect
    public GameObject bulletImpactEffectPrefab;

    // Prefab for blood spray effect
    public GameObject bloodSprayEffect;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Check if an instance of GlobalReferences already exists
        if (Instance != null && Instance != this)
        {
            // If instance already exists, destroy this instance
            Destroy(this.gameObject);
        }
        else
        {
            // Set this instance as the singleton instance
            Instance = this;
        }
    }
}
