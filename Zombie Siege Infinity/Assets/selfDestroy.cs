using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour
{
    public float TimeForDestruction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf(TimeForDestruction));
    }
    
    private IEnumerator DestroySelf(float TimeForDestruction)
    {
        yield return new WaitForSeconds(TimeForDestruction);
        Destroy(gameObject);
    }

    
}
