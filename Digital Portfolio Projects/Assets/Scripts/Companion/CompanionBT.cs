using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CompanionBT : BTree
{
    public Transform playerTransform;
    public Transform arrowTransform;
    public GameObject playerObject; 
    public Rigidbody rb;
    public GameObject arrow;
    public float arrowSpeed = 5;

    public static float speed = 5.0f;
    public static float fovRange = 6f;
    public static float meleeAttackRange = 2f;
    public static float damage = 10f;
    public static float healTimer = 0.0f;
    public static float minRangedAttack = 10f;
    public static float maxRangedAttack = 15f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckCanHeal(transform),
                new CheckPlayerHealth(transform, playerObject), 
                new TaskHealPlayer(transform, playerObject)
            }),
            new Sequence(new List<Node> {
                new CheckEnemyInShootingRange(transform),
                new TaskShootBow(transform, arrowTransform, arrow, arrowSpeed)
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform), 
                new TaskAttackEnemy(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckForEnemy(transform),
                new TaskGoToEnemy(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckCanFollow(transform, playerObject),
                new TaskFollow(transform, playerTransform)
            }),
            new Sequence(new List<Node>
            {
                new CheckCanIdle(transform),
                new TaskIdle(transform, playerTransform)
            })
        });

        return root; 
    }
}
