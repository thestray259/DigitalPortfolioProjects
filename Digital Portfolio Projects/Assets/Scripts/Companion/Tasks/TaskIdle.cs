using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskIdle : Node
{
    Transform transform;
    Transform playerTransform;
    Animator animator;
    float timer = 3.0f;
    bool destSet = false;
    Vector3 destination = new Vector3();
    Vector3 destMin = new Vector3();
    Vector3 destMax = new Vector3();
    Vector3 offset = new Vector3(0.05f, 0.05f, 0.05f);
    Vector3 forward = new Vector3();
    Quaternion rotation = Quaternion.identity;

    public TaskIdle(Transform transform, Transform playerTransform)
    {
        this.transform = transform;
        this.playerTransform = playerTransform;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskIdle");

        timer -= Time.deltaTime;

        // after a couple seconds, choose a place within distance of the Player to move to
        // should add a different destination if ordered to stay -> use canFollow from player
        if (timer <= 0)
        {
            rotation = Quaternion.AngleAxis(Random.Range(-30, 30), Vector3.up);
            rotation = playerTransform.rotation * rotation;
            forward = rotation * Vector3.forward;
            destination = playerTransform.position - forward * Random.Range(1f, 3f);
            destination.y = transform.position.y;
            destMin = destination - offset;
            destMax = destination + offset;
            destSet = true;
            timer = 3.0f;
        }

        if (destSet == true)
        {
            transform.LookAt(destination);
            transform.position = Vector3.MoveTowards(transform.position, destination, CompanionBT.speed * Time.deltaTime);
            animator.SetBool("walking", true);
        }

        if (destSet == true && (transform.position == destination || (transform.position.x >= destMin.x && transform.position.x <= destMax.x &&
                                                  transform.position.y >= destMin.y && transform.position.y <= destMax.y &&
                                                  transform.position.z >= destMin.z && transform.position.z <= destMax.z)))
        {
            timer = 3.0f;
            destSet = false;
            animator.SetBool("walking", false);
            destination = new Vector3();
        }

        state = NodeState.RUNNING;
        return state;
    }
}
