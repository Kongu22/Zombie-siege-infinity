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
     private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
        {
            // If the zombie is already dead, return and do nothing
            if (isDead)
            {
                return;
            }

            HP -= damageAmount;
            if (HP <= 0)
            {
                isDead = true; // Set the flag to true as the zombie is now dead
                int randomValue = Random.Range(0, 2);
                if (randomValue == 0)
                {
                    animator.SetTrigger("DIE1");
                }
                else
                {
                    animator.SetTrigger("DIE2");
                }
                SoundManager.Instance.ZombieChannel.PlayOneShot(SoundManager.Instance.ZombieDeath);
            }
            else
            {
                animator.SetTrigger("DAMAGE");
                SoundManager.Instance.ZombieChannel.PlayOneShot(SoundManager.Instance.ZombieHit);
            }
        }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); // Attack distance

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 20f); // Detection distance

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 25f); // Stop chasing distance
    }

 
}
