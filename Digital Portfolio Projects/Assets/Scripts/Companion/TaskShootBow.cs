using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskShootBow : Node
{
    readonly Transform transform;

    public TaskShootBow(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        // shoot bow at enemy

        state = NodeState.RUNNING;
        return state;
    }
}
