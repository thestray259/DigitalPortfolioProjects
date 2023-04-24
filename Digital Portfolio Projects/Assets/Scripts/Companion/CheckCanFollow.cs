using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class CheckCanFollow : Node
{
    Transform transform;
    public Vector3 destination;
    private UnityEngine.GameObject playerObject;
    Animator animator;

    public CheckCanFollow(Transform transform, GameObject playerObject) { this.transform = transform; this.playerObject = playerObject; animator = transform.GetComponent<Animator>(); }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckCanFollow");
        var playerTransform = playerObject.GetComponent<Transform>();
        var distance = Vector3.Distance(transform.position, playerTransform.position);
        if (playerObject.GetComponent<Player>().canFollow == true && distance >= 3f)
        {
            Debug.Log("Can Follow = true");                
            state = NodeState.SUCCESS;
            return state; 
        }

        Debug.Log("Can Follow = false");
        animator.SetBool("walking", false);
        animator.SetBool("running", false);

        state = NodeState.FAILURE;
        return state; 
    }
}
