using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskShootBow : Node
{
    readonly Transform transform;
    Transform arrowTransform;
    //Transform arrowTarget;
    GameObject arrowPrefab;
    float arrowSpeed;
    //private static readonly int _enemyLayerMask = 1 << 6;

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
        animator.SetBool("walking", false);
        animator.SetBool("running", false);

        Transform target = (Transform)GetData("target");
        var targetTransform = target.GetComponent<TargetTransform>();
        var targetPosition = targetTransform.target.position;
        //Collider[] colliders = Physics.OverlapSphere(target.position, CompanionBT.maxRangedAttack, _enemyLayerMask);

        //Projectile projectile = new Projectile(arrowPrefab, arrowTransform);
        //projectile.transform.LookAt(target.position);
        Quaternion rotation = Quaternion.LookRotation((targetPosition) - arrowTransform.position);
        Projectile projectile = new Projectile(arrowPrefab, arrowTransform.position, rotation);
        transform.LookAt(target);

        Debug.DrawLine(arrowTransform.position, targetPosition, Color.red, 10);

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            Debug.Log("Bow shot");
            animator.SetBool("walking", false);

            projectile.ShootProjectile();

            attackCounter = 0f;
        }        

        state = NodeState.RUNNING;
        return state;
    }
}
