using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class CheckForEnemy : Node
{
    private Transform transform;
    private static int _enemyLayerMask = 1 << 6;

    public CheckForEnemy(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckForEnemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position, CompanionBT.fovRange, _enemyLayerMask); 
        object t = GetData("target"); 
        if (t == null)
        {
            Debug.Log("t == null");
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state; 
            }

            ClearData("target");
            state = NodeState.FAILURE;
            return state; 
        }
        else
        {
            Debug.Log("t != null");
            if (colliders.Length <= 0)
            {
                ClearData("target");
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state; 
    }
}
