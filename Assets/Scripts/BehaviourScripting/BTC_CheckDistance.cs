using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class BTC_CheckDistance : Conditional
{
    public FalseKnightController falseKnight;
    [SerializeField] private SharedFloat range;

    public override void OnAwake()
    {
        falseKnight = GetComponent<FalseKnightController>();
    }

    public override TaskStatus OnUpdate()
    {
        if (falseKnight == null)
        {
            Debug.LogWarning("False Knight°¡ ¾øÀ½");
            return TaskStatus.Failure;
        }

        if (falseKnight.CheckDistance(range.Value))
        { 
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
