using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree; 

public class TaskAttackEnemy : Node
{
    private Transform transform; 
    private Transform lastTarget;

    private float attackTime = 1f;
    private float attackCounter = 0;

    private Animator animator; 

    public TaskAttackEnemy(Transform transform) { this.transform = transform; animator = transform.GetComponent<Animator>(); }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskAttackEnemy"); 
        Transform target = (Transform)GetData("target");

        Collider[] colliders = Physics.OverlapSphere(target.position, CompanionBT.melleAttackRange); // used to be lastTarget.position but that was breaking it for some reason

        if (target != lastTarget)
        {
            lastTarget = target; 
        }

        attackCounter += Time.deltaTime; 
        if (attackCounter >= attackTime)
        {
            animator.SetBool("walking", false); 
            animator.SetTrigger("punch"); 
            foreach (Collider collider in colliders)
            {
                //if (collider.gameObject == component.gameObject) continue; // was breaking it for some reason 

                if (collider.gameObject.activeInHierarchy == true && collider.CompareTag("Enemy"))
                {
                    if (collider.gameObject.TryGetComponent<GenEnemyBT>(out GenEnemyBT genEnemyBT))
                    {
                        genEnemyBT.gameObject.GetComponent<Health>().Damage(CompanionBT.damage);
                    }
                }
            }

            attackCounter = 0f; 
        }

        state = NodeState.RUNNING;
        return state; 
    }
}
