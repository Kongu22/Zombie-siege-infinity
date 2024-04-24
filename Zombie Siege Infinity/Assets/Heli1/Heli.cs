using System.Collections;
using UnityEngine;

public class Heli : MonoBehaviour
{
    private AudioSource heliAudioSource; // Источник звука для вертолёта
    public bool isDead = false;
    public float fadeOutTime = 2f; // Time to fade out the helicopter sound and visuals

    // Start is called before the first frame update
    void Start()
    {
        if (!isDead)
        {
            // Assuming SoundManager and HeliChannel are properly set up and have an AudioSource component
            heliAudioSource = SoundManager.Instance.HeliChannel;
            if (heliAudioSource != null && heliAudioSource.clip != null)
            {
                heliAudioSource.PlayOneShot(heliAudioSource.clip);
                StartCoroutine(StopSoundAndDestroy()); // Start the coroutine
            }
        }
    }

    IEnumerator StopSoundAndDestroy()
    {
        yield return new WaitForSeconds(8); // Wait for 8 seconds before starting the fade out
        StartCoroutine(FadeOut(heliAudioSource, fadeOutTime)); // Fade out the sound
        StartCoroutine(FadeOutGameObject(fadeOutTime)); // Fade out the visual
        yield return new WaitForSeconds(fadeOutTime); // Wait for the fade out to complete
        Destroy(gameObject); // Destroy the helicopter object
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset the volume for future use if needed
    }

    IEnumerator FadeOutGameObject(float fadeTime)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            Color startColor = mat.color;

            while (mat.color.a > 0)
            {
                Color newColor = mat.color;
                newColor.a -= Time.deltaTime / fadeTime;
                mat.color = newColor;
                yield return null;
            }
        }
    }
}
