using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get;  set; }

    public AudioSource shootingSoundPistol92;
    public AudioSource ReloadingSoundPistol92;
    public AudioSource emptyMagazineSound;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
