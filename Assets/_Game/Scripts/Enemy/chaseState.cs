using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chaseState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;
    float distance;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 5;
        animator.transform.LookAt(player);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        distance = Vector3.Distance(player.position, animator.transform.position);
        animator.transform.LookAt(player);
        if (distance > 3f)
        {
            animator.SetBool("isChasing", true);
        }
        else
        {
            animator.SetBool("isAttacking", true);
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);

    }

}
