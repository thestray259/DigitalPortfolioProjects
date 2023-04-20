using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskIdle : Node
{
    Transform transform;
    private UnityEngine.GameObject playerObject;

    public TaskIdle(Transform transform, GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskIdle");
        // idle code here

        state = NodeState.RUNNING;
        return state;
    }
}
