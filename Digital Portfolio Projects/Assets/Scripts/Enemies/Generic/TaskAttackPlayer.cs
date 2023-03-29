using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttackPlayer : Node
{
    private Transform transform; 
    private Transform lastTarget;

    private float attackTime = 1f;
    private float attackCounter = 0;

    private Animator animator; 
    private Animator playerAnimator; 

    public TaskAttackPlayer(Transform transform) { this.transform = transform; animator = transform.GetComponent<Animator>(); }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered TaskAttackPlayer");
        Transform target = (Transform)GetData("target");
        //playerAnimator = target.GetComponent<Animator>();

        if (target != lastTarget)
        {
            lastTarget = target;
        }

        Collider[] colliders = Physics.OverlapSphere(target.position, GenEnemyBT.attackRange + 1);

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            animator.SetBool("walking", false); 
            animator.SetTrigger("punch"); 
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    if (collider.gameObject.TryGetComponent<Player>(out Player player))
                    {
                        player.gameObject.GetComponent<Health>().health -= GenEnemyBT.damage;
                        //playerAnimator.SetTrigger("hit"); 
                        if (player.gameObject.GetComponent<Health>().health <= 0)
                        {
                            ClearData("target");
                            //playerAnimator.SetTrigger("dead"); 
                        }
                    }
                }
            }

            attackCounter = 0f;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
