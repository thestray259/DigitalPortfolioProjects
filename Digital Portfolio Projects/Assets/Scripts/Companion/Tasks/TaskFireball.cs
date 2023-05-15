using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFireball : Node
{
    readonly Transform transform;
    readonly Transform fireTransform;
    readonly GameObject fireballPrefab;
    readonly Animator animator;

    private readonly float attackTime = 1.5f;
    private float attackTimer = 0;

    public TaskFireball(Transform transform)
    {
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {


        state = NodeState.RUNNING;
        return state;
    }
}
