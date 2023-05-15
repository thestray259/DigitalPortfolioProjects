using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree; 

public class CheckEnemyInAttackRange : Node
{
    readonly Transform transform; 

    public CheckEnemyInAttackRange(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Comp entered CheckEnemyInAttackRange");
        object t = GetData("target"); 
        if (t == null)
        {
            Debug.Log("CheckEnemyInAttackRange Failure");
            state = NodeState.FAILURE;
            return state; 
        }

        Transform target = (Transform)t;
        Animator targetAnim = target.GetComponent<Animator>();

        if (Vector3.Distance(transform.position, target.position) <= CompanionBT.meleeAttackRange && !targetAnim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {            
            Debug.Log("CheckEnemyInAttackRange Success");
            state = NodeState.SUCCESS;
            return state; 
        }

        state = NodeState.FAILURE;
        return state; 
    }
}
