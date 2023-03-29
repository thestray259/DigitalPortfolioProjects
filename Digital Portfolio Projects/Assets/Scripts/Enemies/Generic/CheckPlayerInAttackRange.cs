using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class CheckPlayerInAttackRange : Node
{
    private Transform transform; 

    public CheckPlayerInAttackRange(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered CheckPlayerInAttackRange");
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) <= GenEnemyBT.attackRange)
        {
            // set animations here maybe

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
