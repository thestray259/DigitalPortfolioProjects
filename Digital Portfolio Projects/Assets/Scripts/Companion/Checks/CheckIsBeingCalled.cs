using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsBeingCalled : Node
{
    public Transform transform;
    public GameObject playerObject;

    public CheckIsBeingCalled(Transform transform, GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject;
    }

    public override NodeState Evaluate()
    {
        if (playerObject.GetComponent<Player>().isCalled == true)
        {
            Debug.Log("Companion is being called to Player");
            state = NodeState.SUCCESS;
            return state;
        }

        Debug.Log("Companion is NOT being called to Player");
        state = NodeState.FAILURE;
        return state;
    }
}
