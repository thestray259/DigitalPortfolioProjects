using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class TaskHealPlayer : Node
{
    private Transform transform;
    private UnityEngine.GameObject playerObject;

    public TaskHealPlayer(Transform transform, UnityEngine.GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskHealPlayer");

        // add a heal amount to player health 

        playerObject.GetComponent<Health>().health += 20; 

        state = NodeState.SUCCESS;
        return state; 
    }
}
