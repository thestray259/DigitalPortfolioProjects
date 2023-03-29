using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class CheckForPlayer : Node
{
    private Transform transform;
    private static int _playerLayerMask = 1 << 7;
    float timer = 0.0f;
    //public Collider[] colliders; 

    public CheckForPlayer(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        TaskGoToPlayer task = new TaskGoToPlayer(transform);
        Debug.Log("Enemy entered CheckForPlayer");
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, GenEnemyBT.fovRange, _playerLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }

/*            if (task.timer >= 3.0f)
            {
                t = null;
                colliders = null; 
                state = NodeState.FAILURE;
                return state;
            }*/

            state = NodeState.FAILURE;
            return state;
        }



        state = NodeState.SUCCESS;
        return state;
    }
}
