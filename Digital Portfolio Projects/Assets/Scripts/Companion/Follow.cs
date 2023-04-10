using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree; 

public class Follow : Node
{
    private Transform transform;
    private Transform playerTransform;

    private Animator animator; 

    public Follow(Transform transform, Transform playerTransform)
    {
        this.transform = transform;
        this.playerTransform = playerTransform;
        animator = transform.GetComponent<Animator>(); 
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered Follow");
        Vector3 destination = new Vector3();

        Debug.Log("Walking Bool: " + animator.GetBool("walking"));         

        if (Vector3.Distance(transform.position, playerTransform.position) > 3.0f)
        {
            //if (animator.GetBool("walking") == false)
            {
                Quaternion rotation = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.up);
                Vector3 forward = rotation * Vector3.forward;
                destination = transform.position + forward * Random.Range(3f, 5);
            }

            animator.SetBool("walking", true);
            // change so that comp picks a position behind player to move towards
            transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
            transform.LookAt(destination);
        }
        else { animator.SetBool("walking", false); }

        state = NodeState.RUNNING;
        return state; 
    }
}
