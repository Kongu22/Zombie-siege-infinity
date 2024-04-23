using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;
    public float chaseSpeed = 6f;
    public float stopChaseDistance = 24;
    public float attackDistance = 2.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //--- initialize the chase state ---//

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         if(SoundManager.Instance.ZombieChannel.isPlaying == false)
        {
            SoundManager.Instance.ZombieChannel.PlayOneShot(SoundManager.Instance.ZombieChase);
        }

        //--- move the zombie to the player ---//

       agent.SetDestination(player.position);
       animator.transform.LookAt(player);

        //--- Transition to attack state ---//

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        
        if (distanceFromPlayer > stopChaseDistance)
        {
            animator.SetBool("isChasing", false);
        }

        //--- Transition to patroling state ---//

        if (distanceFromPlayer < attackDistance) 
        {
            animator.SetBool("isAttacking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //--- reset the chase state ---//

        agent.SetDestination(animator.transform.position);
        SoundManager.Instance.ZombieChannel.Stop();
    }

}
