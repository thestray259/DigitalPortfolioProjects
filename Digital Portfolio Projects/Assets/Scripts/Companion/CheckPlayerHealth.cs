using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class CheckPlayerHealth : Node
{
    private Transform transform;
    private UnityEngine.GameObject playerObject;

    public CheckPlayerHealth(Transform transform, UnityEngine.GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject; 
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckPlayerHealth"); 
        // check if player health is below a certain point/percentage 
        // if yes, return success 
        // else return failure 

        // check heal timer
        // if <= 0, then check player health 
        // else return failure 

        if (playerObject.GetComponent<Health>().health <= 50)
        {
            CompanionBT.healTimer = 5.0f; 
            state = NodeState.SUCCESS;
            return state; 
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }
}
