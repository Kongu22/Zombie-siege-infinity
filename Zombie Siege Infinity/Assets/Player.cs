using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Make sure this using directive is included for handling UI elements.

public class Player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;
    public TextMeshProUGUI PlayerHealthUI;  // Ensure this is assigned in the Unity Inspector.
    public GameObject GameOverUI;  // Ensure this is assigned in the Unity Inspector.

    public bool isDead;

   private void Start()
   {
         PlayerHealthUI.text = $"Health: {HP}"; // Initialize health display.
   }

   public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            print("Player is dead!");
            PlayerDead();
            isDead = true;
        }
        else
        {
            print("Player HIT!");
            StartCoroutine(ShowBloodyScreen());
            PlayerHealthUI.text = $"Health: {HP}"; // Update health display when taking damage.
            // PlayerHealthUI.gameObject.SetActive(true);
        }
    }

    private IEnumerator ShowBloodyScreen()
    {
        if(!bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(true);
        }
        
        Image image = bloodyScreen.GetComponentInChildren<Image>();
        if (image == null)
        {
            Debug.LogError("No Image component found in bloodyScreen's children!");
            yield break; // Exit if no Image component is found.
        }
 
        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;
 
        float duration = 3f;
        float elapsedTime = 0f;
 
        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
 
            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
 
            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;
 
            yield return null; ; // Wait for the next frame.
        }
        if(bloodyScreen.activeInHierarchy )
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void PlayerDead()
    {
        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        // dying animation
        GetComponentInChildren<Animator>().enabled = true;

        if(isDead == true)
        {
            GetComponent<ScreenBlackOut>().StartFade();
            StartCoroutine(ShowGameOverUI());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);

            if (HP <= 0)
            {
                PlayerHealthUI.text = $"Health: {0}";
            }


        }
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        GameOverUI.gameObject.SetActive(true);
    }
}


