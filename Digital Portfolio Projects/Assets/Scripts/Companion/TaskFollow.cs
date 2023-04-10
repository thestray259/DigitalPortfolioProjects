using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree; 

public class TaskFollow : Node
{
    private Transform transform;
    private Transform playerTransform;
    bool destSet = false;
    Vector3 destination = new Vector3();

    private Animator animator; 

    public TaskFollow(Transform transform, Transform playerTransform)
    {
        this.transform = transform;
        this.playerTransform = playerTransform;
        animator = transform.GetComponent<Animator>(); 
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered Follow");
        Debug.Log("Walking Bool: " + animator.GetBool("walking"));

        if (Vector3.Distance(transform.position, playerTransform.position) > 3.0f && destSet == false)
        {
            Quaternion rotation = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.up);
            Vector3 forward = rotation * Vector3.forward;
            destination = playerTransform.position + forward * Random.Range(3f, 5);
            Debug.Log("First Destination: " + destination);
            destSet = true;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) >= 3.0f && destSet == true)
        {
            animator.SetBool("walking", true);
            transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
            transform.LookAt(destination);
            Debug.Log("Walking to " + destination);
        }
        else if (Vector3.Distance(destination, playerTransform.position) > 3.0f)
        {
            Quaternion rotation = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.up);
            Vector3 forward = rotation * Vector3.forward;
            destination = playerTransform.position + forward * Random.Range(3f, 5);
            Debug.Log("Reset Destination: " + destination);
            animator.SetBool("walking", true);
            transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
            transform.LookAt(destination);
        }
        else if (transform.position == destination) { animator.SetBool("walking", false); destSet = false; }
        else { animator.SetBool("walking", false); destSet = false; Debug.Log("Following else"); }

        state = NodeState.RUNNING;
        return state; 
    }
}
