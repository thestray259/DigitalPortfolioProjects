using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBeingCalled : Node
{
    public Transform transform;
    public GameObject playerObject;

    private Transform playerTransform;
    private Vector3 destination;
    private Vector3 forward;
    private Quaternion rotation = Quaternion.identity;
    private Animator animator;

    public TaskBeingCalled(Transform transform, GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject;
        playerTransform = playerObject.GetComponent<Transform>();
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskBeingCalled");

        if (destination == Vector3.zero)
        {
            rotation = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.up);
            rotation = playerTransform.rotation * rotation;
            forward = rotation * Vector3.forward;
            destination = playerTransform.position + forward * Random.Range(3f, 4);
            destination.y = transform.position.y;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) > 3f)
        {
            transform.LookAt(destination);
            transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
            animator.SetBool("walking", true);
        }
        else playerObject.GetComponent<Player>().isCalled = false;

        state = NodeState.RUNNING;
        return state;
    }
}
