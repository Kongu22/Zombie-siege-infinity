using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Make sure this using directive is included for handling UI elements.

public class Player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;
    public TextMeshProUGUI PlayerHealthUI;  // Ensure this is assigned in the Unity Inspector.
    public GameObject GameOverUI;  // Ensure this is assigned in the Unity Inspector.

    public bool isDead = false;

   private void Start()
   {
         PlayerHealthUI.text = $"Health: {HP}"; // Initialize health display.
   }

  public void TakeDamage(int damageAmount)
    {
        if (isDead) return;  // Early return if player is already dead

        HP -= damageAmount;
        PlayerHealthUI.text = $"Health: {HP}";  // Update health display always

        if (HP <= 0)
        {
            if (!isDead)
            {
                isDead = true;  // Mark the player as dead
                print("Player is dead!");
                PlayerDead();
                PlayerHealthUI.text = "Health: 0";
                SoundManager.Instance.PlayerChannel.PlayOneShot(SoundManager.Instance.PlayerDeath);

            }
        }
        else
        {
            print("Player HIT!");
            StartCoroutine(ShowBloodyScreen());
            SoundManager.Instance.PlayerChannel.PlayOneShot(SoundManager.Instance.PlayerHit);
        }
    }

   private IEnumerator ShowBloodyScreen()
    {
        if (!bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(true);
        }

        Image image = bloodyScreen.GetComponentInChildren<Image>();
        if (image == null)
        {
            Debug.LogError("No Image component found in bloodyScreen's children!");
            yield break;
        }

        float duration = 3f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bloodyScreen.SetActive(false);
    }

private void PlayerDead()
{
    // Ensure the PlayerChannel is not playing anything else
    SoundManager.Instance.PlayerChannel.Stop();
    
    // Set the new clip for DeathMusic
    SoundManager.Instance.PlayerChannel.clip = SoundManager.Instance.DeathMusic;

    // Play the DeathMusic after a 2-second delay
    SoundManager.Instance.PlayerChannel.PlayDelayed(2f);
    WeaponManager.Instance.DisableWeapons();

    // Check if the player has a weapon and disable weapon visuals if they do
    Weapon weapon = GetComponentInChildren<Weapon>();
    if (weapon != null)
    {
        weapon.gameObject.SetActive(false);
    }

    GetComponent<MouseMovement>().enabled = false;
    GetComponent<PlayerMovement>().enabled = false;

    // Start the blackout and game over UI only once
    GetComponent<ScreenBlackOut>().StartFade();
    StartCoroutine(ShowGameOverUI());
    GetComponentInChildren<Animator>().enabled = true;
}

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);  // Delay to ensure blackout transitions smoothly
        GameOverUI.SetActive(true);

        // Save the high score
        int waveSurvived = GlobalReferences.Instance.WaveNumber;

        if (waveSurvived - 1 > SaveLoadManager.Instance.LoadHighScore())
        {
            SaveLoadManager.Instance.SaveHighScore(waveSurvived - 1);
        }

        StartCoroutine(ReturnToMainMenu());
        
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand") && !isDead)
        {
            int damage = other.gameObject.GetComponent<ZombieHand>().damage;
            TakeDamage(damage);
        }
    }
}

