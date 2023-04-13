using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;
using Unity.VisualScripting;
using static Unity.VisualScripting.Member;

public class TaskFollow : Node
{
    private Transform transform;
    private Transform playerTransform;
    bool destSet = false;
    Vector3 destination = new Vector3();
    Vector3 destMin = new Vector3();
    Vector3 destMax = new Vector3();
    Vector3 offset = new Vector3(0.1f, 0.1f, 0.1f);
    Vector3 forward = new Vector3();
    Quaternion rotation = Quaternion.identity;

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

        if (destSet == false && Vector2.Distance(transform.position, playerTransform.position) > 2.0f)
        {
            transform.LookAt(playerTransform.position);
            rotation = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.up);
            forward = rotation * Vector3.forward;
            destination = playerTransform.position + forward * Random.Range(3f, 4);
            destination.y = transform.position.y;
            destMin = destination - offset;
            destMax = destination + offset;
            Debug.Log("First Destination: " + destination);
            destSet = true;
        }
        
        if (destSet == true && Vector2.Distance(transform.position, playerTransform.position) >= 2.0f)
        {
            animator.SetBool("walking", true);
            transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
            transform.LookAt(destination);
            Debug.Log("Walking to " + destination);
            Debug.DrawLine(transform.position, destination);
            if (transform.position.x >= destMin.x && transform.position.x <= destMax.x &&
                 transform.position.y >= destMin.y && transform.position.y <= destMax.y &&
                 transform.position.z >= destMin.z && transform.position.z <= destMax.z)
            { animator.SetBool("walking", false); destSet = false; Debug.Log("Walking stopped"); }
            else if (Vector2.Distance(destination, playerTransform.position) > 3.0f)
            {
                transform.LookAt(playerTransform.position);
                rotation = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.up);
                forward = rotation * Vector3.forward;
                destination = playerTransform.position + forward * Random.Range(3f, 4);
                destination.y = transform.position.y;
                destMin = destination - offset;
                destMax = destination + offset;
                Debug.Log("Reset Destination in First: " + destination);
                Debug.DrawLine(transform.position, destination);
                animator.SetBool("walking", true);
                transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
                transform.LookAt(destination);
            }
        }
        else { animator.SetBool("walking", false); destSet = false; Debug.Log("Following else"); }

        state = NodeState.RUNNING;
        return state; 
    }
}
