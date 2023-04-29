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

    public TaskAttackPlayer(Transform transform) { this.transform = transform; animator = transform.GetComponent<Animator>(); }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered TaskAttackPlayer");
        Transform target = (Transform)GetData("target");

        if (target != lastTarget)
        {
            lastTarget = target;
        }

        Collider[] colliders = Physics.OverlapSphere(target.position, GenEnemyBT.attackRange + 1, CheckForPlayerAndCompanion._playerLayerMask);
        Collider[] compColliders = Physics.OverlapSphere(target.position, GenEnemyBT.attackRange + 1, CheckForPlayerAndCompanion._companionLayerMask);

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            animator.SetBool("walking", false); 
            animator.SetTrigger("punch"); 
            if (colliders.Length > 0)
            {
                if (colliders[0].CompareTag("Player"))
                {
                    if (colliders[0].gameObject.TryGetComponent(out Player player))
                    {
                        Debug.Log("Enemy Attacking Player");
                        player.gameObject.GetComponent<Health>().Damage(GenEnemyBT.damage);
                        if (player.gameObject.GetComponent<Health>().health <= 0)
                        {
                            // might need to do something different here bc companion
                            ClearData("target");
                        }
                    }
                }
            }
            else if (compColliders.Length > 0)
            {
                if (compColliders[0].CompareTag("Companion"))
                {
                    if (compColliders[0].gameObject.TryGetComponent(out CompanionBT companionBT))
                    {
                        Debug.Log("Enemy Attacking Companion");
                        companionBT.GetComponent<Health>().Damage(GenEnemyBT.damage);
                        if (companionBT.GetComponent<Health>().health <= 0)
                        {
                            ClearData("target");
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
