using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree; 

public class CheckPlayerHealth : Node
{
    private Transform transform;
    private GameObject playerObject;

    public CheckPlayerHealth(Transform transform, GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject; 
    }

    public override NodeState Evaluate()
    {
        // check if player health is below a certain point/percentage 
        // if yes, return success, else return failure 

        if (playerObject.GetComponent<Health>().health <= 50)
        {
            Debug.Log("Companion is Healing Player");
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
