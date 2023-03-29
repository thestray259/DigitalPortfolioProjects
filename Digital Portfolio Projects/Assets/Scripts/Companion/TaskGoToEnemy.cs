using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class TaskGoToEnemy : Node
{
    private Transform transform;

    private Animator animator; 

    public TaskGoToEnemy(Transform transform) { this.transform = transform; animator = transform.GetComponent<Animator>(); }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskGoToEnemy");
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            animator.SetBool("walking", true);
            // change so that comp sees where player is and stays a distance away
            transform.position = Vector3.MoveTowards(transform.position, target.position, CompanionBT.speed * Time.deltaTime);
            transform.LookAt(target.position);
        }
        else animator.SetBool("walking", false); 

        state = NodeState.RUNNING;
        return state; 
    }
}
