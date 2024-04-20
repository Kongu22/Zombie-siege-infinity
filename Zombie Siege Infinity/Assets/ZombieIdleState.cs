using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{
    float timer;
    public float idleTime;
    Transform player;

    public float detectionAreaRadius = 25f;

    //enter the idle state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //update the idle state every frame
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      // --- Transition to chase state if player is detected ---//

        timer += Time.deltaTime;
        if (timer >= idleTime)
        {
            animator.SetBool("isPatroling", true);
        } 

        // --- Transition to chase state if player is detected ---//

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }

}
