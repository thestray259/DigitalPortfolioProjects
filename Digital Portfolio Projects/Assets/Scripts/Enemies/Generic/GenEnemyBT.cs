using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class GenEnemyBT : BTree
{
    public static float speed = 5.0f;
    public static float fovRange = 6f;
    public static float attackRange = 1.3f;

    public static float damage = 10f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInAttackRange(transform),
                new TaskAttackPlayer(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckForPlayer(transform),
                new TaskGoToPlayer(transform)
            }),
            new GenEnemyIdle(transform)
        });

        return root; 
    }

/*    private void Update()
    {
        if (_root != null) _root.Evaluate();
        animator.SetFloat("speed", movement.velocity.magnitude);
        Debug.Log("Enemy Speed: " + movement.velocity.magnitude); 
    }*/
}
