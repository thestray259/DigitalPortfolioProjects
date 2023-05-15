using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanIdle : Node
{
    Transform transform;
    Animator animator;

    public CheckCanIdle(Transform transform)
    {
        this.transform = transform;
        this.animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckCanIdle");
        if (animator.GetBool("walking") == true || animator.GetBool("running") == true)
        {
            Debug.Log("Idle Failure");
            state = NodeState.FAILURE;
            return state;
        }

        Debug.Log("Idle Success");
        state = NodeState.SUCCESS;
        return state;
    }
}
