using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree; 

public class CheckCanHeal : Node
{
    Transform transform;

    public CheckCanHeal(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        if (CompanionBT.healTimer <= 0f)
        {
            state = NodeState.SUCCESS;
            return state; 
        }

        CompanionBT.healTimer -= Time.deltaTime;

        state = NodeState.FAILURE;
        return state; 
    }
}
