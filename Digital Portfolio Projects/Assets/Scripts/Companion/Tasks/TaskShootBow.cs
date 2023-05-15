using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskShootBow : Node
{
    readonly Transform transform;
    readonly Transform arrowTransform;
    readonly GameObject arrowPrefab;

    private readonly float attackTime = 1.5f;
    private float attackCounter = 0;
    private readonly Animator animator;

    public TaskShootBow(Transform transform, Transform arrowTransform, GameObject arrow)
    {
        this.transform = transform;
        this.arrowTransform = arrowTransform;
        this.arrowPrefab = arrow;

        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        // shoot bow at enemy
        Debug.Log("Comp shooting bow...");
        animator.SetBool("walking", false);
        animator.SetBool("running", false);

        Transform target = (Transform)GetData("target");
        var targetTransform = target.GetComponent<TargetTransform>();
        var targetPosition = targetTransform.target.position;

        Quaternion rotation = Quaternion.LookRotation((targetPosition) - arrowTransform.position);
        Projectile projectile = new Projectile(arrowPrefab, arrowTransform.position, rotation);
        transform.LookAt(target);

        Debug.DrawLine(arrowTransform.position, targetPosition, Color.red, 10);

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            Debug.Log("Bow shot");
            animator.SetBool("walking", false);
            animator.SetBool("running", false);
            animator.SetTrigger("shootBow");
            projectile.ShootProjectile();

            attackCounter = 0f;
        }        

        state = NodeState.RUNNING;
        return state;
    }
}
