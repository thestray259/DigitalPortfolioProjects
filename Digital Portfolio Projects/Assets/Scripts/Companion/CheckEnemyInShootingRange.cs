using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyInShootingRange : Node
{
    private Transform transform;
    private static readonly int _enemyLayerMask = 1 << 6;

    public CheckEnemyInShootingRange(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Comp entered CheckEnemyInShootingRange");
        Collider[] colliders = Physics.OverlapSphere(transform.position, CompanionBT.maxRangedAttack, _enemyLayerMask);
        object t = GetData("target");
        if (t == null)
        {
            if (colliders.Length > 0)
            {
                Debug.Log("Companion CheckEnemyInShootingRange Success");
                parent.parent.SetData("target", colliders[0].transform);
                state = NodeState.SUCCESS;
                return state;
            }

            Debug.Log("CheckEnemyInShootingRange Failure -> t == null");
            ClearData("target");
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

        Debug.Log("CheckEnemyInShootingRange Failure -> out of range");
        //ClearData("target");
        state = NodeState.FAILURE;
        return state;
    }
}
