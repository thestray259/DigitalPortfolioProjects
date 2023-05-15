using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree; 

public class TaskHealPlayer : Node
{
    private Transform transform;
    private GameObject playerObject;

    public TaskHealPlayer(Transform transform, GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject;
    }

    public override NodeState Evaluate()
    {
        playerObject.GetComponent<Health>().health += 20; 
        state = NodeState.SUCCESS;
        return state; 
    }
}
