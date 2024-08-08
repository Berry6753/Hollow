using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Idle : FalseKnightAction
{
    private readonly int _hashTrace = Animator.StringToHash("Trace");

    public override void OnStart()
    {
        _anim.SetBool(_hashTrace, false);
    }

    public override TaskStatus OnUpdate()
    {


        return TaskStatus.Success;
    }
}
