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
        object t = GetData("target"); 
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, CompanionBT.fovRange, _enemyLayerMask); 

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state; 
            }

            state = NodeState.FAILURE;
            return state; 
        }

        state = NodeState.SUCCESS;
        return state; 
    }
}
