using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanIdle : Node
{
    Transform transform;
    private UnityEngine.GameObject playerObject;
    Animator animator;

    public CheckCanIdle(Transform transform, GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject;
        this.animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckCanIdle");
        animator.SetBool("walking", false);
        animator.SetBool("running", false);

        Debug.Log("Idle Success");
        state = NodeState.SUCCESS;
        return state;
    }
}
