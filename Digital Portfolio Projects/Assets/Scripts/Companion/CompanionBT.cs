using System.Collections.Generic;
using BehaviorTree; 

public class CompanionBT : BTree
{
    public UnityEngine.Transform playerTransform;
    public UnityEngine.GameObject playerObject; 

    public static float speed = 5.0f;
    public static float fovRange = 6f;
    public static float attackRange = 2f;
    public static float damage = 10f;
    public static float healTimer = 0.0f; 

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
                new Follow(transform, playerTransform)
            })            
        });

        return root; 
    }
}
