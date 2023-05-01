using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyInShootingRange : Node
{
    private Transform transform;

    public CheckEnemyInShootingRange(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) >= CompanionBT.minRangedAttack && Vector3.Distance(transform.position, target.position) <= CompanionBT.maxRangedAttack)
        {
            Debug.Log("Companion CheckEnemyInShootingRange Success");
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
