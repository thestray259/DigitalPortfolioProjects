using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskShootBow : Node
{
    readonly Transform transform;
    Transform arrowTransform;
    Transform arrowTarget;
    GameObject arrowPrefab;
    float arrowSpeed;
    private static readonly int _enemyLayerMask = 1 << 6;

    private float attackTime = 1.5f;
    private float attackCounter = 0;
    private Animator animator;

    public TaskShootBow(Transform transform, Transform arrowTransform, GameObject arrow, float arrowSpeed)
    {
        this.transform = transform;
        this.arrowTransform = arrowTransform;
        this.arrowPrefab = arrow;
        this.arrowSpeed = arrowSpeed;

        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        // shoot bow at enemy
        Debug.Log("Comp shooting bow...");
        Transform target = (Transform)GetData("target");
        Collider[] colliders = Physics.OverlapSphere(target.position, CompanionBT.maxRangedAttack, _enemyLayerMask);
        Projectile projectile = new Projectile(arrowPrefab, arrowTransform, Space.Self, arrowSpeed);
        transform.LookAt(target);

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            Debug.Log("Bow shot");
            animator.SetBool("walking", false);

            // set trigger for shoot bow anim
            //projectile.ShootProjectile();
            /*            GameObject arrow = Object.Instantiate(arrowPrefab, arrowTransform.position, arrowTransform.rotation);

                        // arrowTarget = enemy being shot at
                        //arrowTarget.position = colliders[0].transform.position;

                        arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, colliders[0].transform.position, arrowSpeed * Time.deltaTime); //arrow.transform.Translate(arrowSpeed * Time.deltaTime * arrowDirection, companionBT.space);
                        Debug.Log("arrow transform: " + arrow.transform.position);*/

            projectile.ShootProjectile();

            attackCounter = 0f;
        }        

        state = NodeState.RUNNING;
        return state;
    }
}
