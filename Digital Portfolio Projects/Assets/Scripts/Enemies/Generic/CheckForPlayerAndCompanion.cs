using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using System.Linq;

public class CheckForPlayerAndCompanion : Node
{
    private Transform transform;
    public static int _playerLayerMask = 1 << 7;
    public static int _companionLayerMask = 1 << 8;

    // share info with TaskAttackPlayer ?
    //public Collider[] colliders;
    //public Collider[] compColliders;

    public CheckForPlayerAndCompanion(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered CheckForPlayerAndCompanion");
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, GenEnemyBT.fovRange, _playerLayerMask);
            Collider[] compColliders = Physics.OverlapSphere(transform.position, GenEnemyBT.fovRange, _companionLayerMask);
            if (colliders.Length > 0)
            {
                Debug.Log("Enemy Found Player");
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }
            else if (compColliders.Length > 0)
            {
                Debug.Log("Enemy Found Companion");
                parent.parent.SetData("target", compColliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                Debug.Log("Enemy Didn't Find Player or Companion");
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
