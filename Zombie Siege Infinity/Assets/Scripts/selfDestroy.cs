using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour
{
    public float TimeForDestruction;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to destroy the game object after a specified time
        StartCoroutine(DestroySelf(TimeForDestruction));
    }
    
    // Coroutine to destroy the game object after a specified time
    private IEnumerator DestroySelf(float TimeForDestruction)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(TimeForDestruction);
        
        // Destroy the game object
        Destroy(gameObject);
    }
}
