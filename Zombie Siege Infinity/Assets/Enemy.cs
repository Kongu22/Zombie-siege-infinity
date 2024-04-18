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
                print("Zombie is dead!");
            }
            else
            {
                animator.SetTrigger("DAMAGE");
                print("Zombie took damage!");
            }
        }
    

//
    private void Update()
    {
        // Check if the zombie is moving or not and set the animation accordingly
        if (navAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
