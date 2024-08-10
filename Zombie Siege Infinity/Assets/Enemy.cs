using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;
    private NavMeshAgent navAgent;
    private CapsuleCollider[] capsuleColliders; 
    public bool isDead = false;
    public bool isBoss = false; // Add this property

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the enemy object
        animator = GetComponent<Animator>();
        // Get the NavMeshAgent component attached to the enemy object
        navAgent = GetComponent<NavMeshAgent>();
        // Get all the CapsuleCollider components attached to the enemy object
        capsuleColliders = GetComponents<CapsuleCollider>();
    }

    // Method to handle taking damage
    public void TakeDamage(int damageAmount)
    {
        // If the zombie is already dead, return and do nothing
        if (isDead)
        {
            return;
        }

        // Reduce the enemy's HP by the damage amount
        HP -= damageAmount;
        if (HP <= 0)
        {
            // Set the flag to true as the zombie is now dead
            isDead = true;
            // deactivate the capsule collider
            foreach (CapsuleCollider capsuleCollider in capsuleColliders)
            {
                capsuleCollider.enabled = false;
            }

            // Generate a random value between 0 and 1
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0)
            {
                // Trigger the "DIE1" animation in the animator
                animator.SetTrigger("DIE1");
            }
            else
            {
                // Trigger the "DIE2" animation in the animator
                animator.SetTrigger("DIE2");
            }
            // Play the zombie death sound
            SoundManager.Instance.ZombieChannel.PlayOneShot(SoundManager.Instance.ZombieDeath);

            // Add coins to the player
            if (isBoss)
            {
                CoinManager.Instance.AddCoins(Random.Range(150, 351));
            }
            else
            {
                CoinManager.Instance.AddCoins(Random.Range(10, 51));
            }
        }
        else
        {
            // Trigger the "DAMAGE" animation in the animator
            animator.SetTrigger("DAMAGE");
            // Play the zombie hit sound
            SoundManager.Instance.ZombieChannel.PlayOneShot(SoundManager.Instance.ZombieHit);
        }
    }

    // Method to draw gizmos for attack distance, detection distance, and stop chasing distance
    private void OnDrawGizmos()
    {
        // Set the color of the gizmo to red
        Gizmos.color = Color.red;
        // Draw a wire sphere at the enemy's position with a radius of 2.5f (attack distance)
        Gizmos.DrawWireSphere(transform.position, 2.5f);

        // Set the color of the gizmo to green
        Gizmos.color = Color.green;
        // Draw a wire sphere at the enemy's position with a radius of 120f (detection distance)
        Gizmos.DrawWireSphere(transform.position, 120f);

        // Set the color of the gizmo to blue
        Gizmos.color = Color.blue;
        // Draw a wire sphere at the enemy's position with a radius of 121f (stop chasing distance)
        Gizmos.DrawWireSphere(transform.position, 121f);
    }
}