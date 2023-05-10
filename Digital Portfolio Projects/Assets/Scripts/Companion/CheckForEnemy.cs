using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class CheckForEnemy : Node
{
    private Transform transform;
    private static readonly int _enemyLayerMask = 1 << 6;

    public CheckForEnemy(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckForEnemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position, CompanionBT.fovRange, _enemyLayerMask); 
        object t = GetData("target"); 
        if (t == null)
        {
            Debug.Log("Check for enemy: t == null");
            if (colliders.Length > 0)
            {
                Debug.Log("CheckForEnemy Success");
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state; 
            }

            Debug.Log("CheckForEnemy Failure");
            ClearData("target");
            state = NodeState.FAILURE;
            return state; 
        }
        else
        {
            Debug.Log("t != null");
            if (colliders.Length <= 0)
            {
                Debug.Log("CheckForEnemy Failure");
                ClearData("target");
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state; 
    }
}
