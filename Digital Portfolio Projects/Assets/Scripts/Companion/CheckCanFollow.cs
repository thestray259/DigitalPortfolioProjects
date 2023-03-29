using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class CheckCanFollow : Node
{
    Transform transform;
    private UnityEngine.GameObject playerObject;

    public CheckCanFollow(Transform transform, GameObject playerObject) { this.transform = transform; this.playerObject = playerObject; }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckCanFollow"); 
        if (playerObject.GetComponent<Player>().canFollow == true)
        {
            Debug.Log("Can Follow = true"); 
            state = NodeState.SUCCESS;
            return state; 
        }

        Debug.Log("Can Follow = false"); 
        state = NodeState.FAILURE;
        return state; 
    }
}
