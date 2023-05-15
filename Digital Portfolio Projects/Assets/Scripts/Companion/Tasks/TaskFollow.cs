using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

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
    float distance;

    private Animator animator;

    public TaskFollow(Transform transform, Transform playerTransform)
    {
        this.transform = transform;
        this.playerTransform = playerTransform;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskFollow");
        distance = Vector3.Distance(transform.position, playerTransform.position);
        if (destSet == false)
        {
            transform.LookAt(playerTransform.position);
            rotation = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.up);
            rotation = playerTransform.rotation * rotation;
            forward =  rotation * Vector3.forward;
            destination = playerTransform.position + forward * Random.Range(3f, 4);
            destination.y = transform.position.y;
            destMin = destination - offset;
            destMax = destination + offset;
            destSet = true;
        }
        
        if (destSet == true && distance >= 3.0f)
        {
            if (distance >= 5) CompanionBT.speed = 9.5f;
            else CompanionBT.speed = 5;

            if (CompanionBT.speed <= 5)
            {
                animator.SetBool("walking", true);
                animator.SetBool("running", false);
            }
            else if (CompanionBT.speed > 5)
            {
                animator.SetBool("walking", false);
                animator.SetBool("running", true);
            }
            Debug.Log("Following");
            transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
            transform.LookAt(destination);
            Debug.DrawLine(transform.position, destination, color:Color.red);

            if (transform.position.x >= destMin.x && transform.position.x <= destMax.x &&
                transform.position.y >= destMin.y && transform.position.y <= destMax.y &&
                transform.position.z >= destMin.z && transform.position.z <= destMax.z)
            { animator.SetBool("walking", false); animator.SetBool("running", false); destSet = false; Debug.Log("Companion within offset"); }
            else if (Vector3.Distance(destination, playerTransform.position) > 3.8f)
            {
                Debug.Log("Follow Destination Reset");
                transform.LookAt(playerTransform.position);
                rotation = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.up);
                rotation = playerTransform.rotation * rotation;
                forward = rotation * Vector3.forward;
                destination = playerTransform.position + forward * Random.Range(3f, 4);
                destination.y = transform.position.y;
                destMin = destination - offset;
                destMax = destination + offset;
                destSet = true;
                Debug.DrawLine(transform.position, destination);
                if (CompanionBT.speed <= 5) animator.SetBool("walking", true);
                else if (CompanionBT.speed > 5) animator.SetBool("running", true);
                transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
                transform.LookAt(destination);
            }
        }
        else { animator.SetBool("walking", false); animator.SetBool("running", false); destSet = false; Debug.Log("Following else"); }

        state = NodeState.RUNNING;
        return state; 
    }
}
