using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class BTC_CheckDistance : FalseKnightConditional
{
    [SerializeField] private SharedFloat _range;

    public override TaskStatus OnUpdate()
    {
        if (falseKnight == null)
        {
            Debug.LogWarning("False Knight°¡ ¾øÀ½");
            return TaskStatus.Failure;
        }

        if (falseKnight.CheckDistance(_range.Value))
        { 
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
