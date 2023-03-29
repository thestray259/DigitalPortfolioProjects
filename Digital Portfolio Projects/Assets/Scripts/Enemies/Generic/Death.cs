using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class Death : Node
{
    private Transform transform; 

    public Death(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        return base.Evaluate();
    }
}
