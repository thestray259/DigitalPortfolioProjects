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
        Debug.Log("Companion entered CheckCanHeal"); 
        // check healTimer 
        // if <= 0, then success 
        // else fail 

        if (CompanionBT.healTimer <= 0f)
        {
            Debug.Log("CheckCanHeal Success"); 
            state = NodeState.SUCCESS;
            return state; 
        }

        Debug.Log("CheckCanHeal Failure"); 
        CompanionBT.healTimer -= Time.deltaTime;
        Debug.Log("healTimer: " + CompanionBT.healTimer); 

        state = NodeState.FAILURE;
        return state; 
    }
}
