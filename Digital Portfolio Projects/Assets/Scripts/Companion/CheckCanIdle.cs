using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanIdle : Node
{
    Transform transform;
    private UnityEngine.GameObject playerObject;

    public CheckCanIdle(Transform transform, GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckCanIdle");
        // do check here, if can return success, else return failure

        state = NodeState.FAILURE;
        return state;
    }
}
