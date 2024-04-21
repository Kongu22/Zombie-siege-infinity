using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePatrolingState : StateMachineBehaviour
{
    float timer;
    public float patrolingTime = 10f;
    Transform player;
    NavMeshAgent agent;

    public float detectionArea = 25f;
    public float patrolSpeed = 2f;
    List<Transform> waypointsList = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //--- initialize the patroling state ---//

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = patrolSpeed;

        timer = 0;

        //--- move the zombie to the first waypoint ---//

        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform waypoint in waypointCluster.transform)
        {
            waypointsList.Add(waypoint);
        }

        Vector3 randomPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(randomPosition);
       
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(SoundManager.Instance.ZombieChannel.isPlaying == false)
        {
            SoundManager.Instance.ZombieChannel.PlayOneShot(SoundManager.Instance.ZombieWalking);
            SoundManager.Instance.ZombieChannel.PlayDelayed(1f);
        }
       //--- check if arived at the waypoint ---//

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }

        //--- Transition to idle state ---//

        timer += Time.deltaTime;
        if (timer >= patrolingTime)
        {
            animator.SetBool("isPatroling", false);
        }

        //--- Transition to chase state ---//

        
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //--- stop the zombie from moving ---//
       agent.SetDestination(animator.transform.position);
       SoundManager.Instance.ZombieChannel.Stop();
    }

}
