using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class BTA_Move : FalseKnightAction
{
    [SerializeField] private SharedFloat _speed;

    private readonly int _hashTrace = Animator.StringToHash("Trace");

    public override void OnStart()
    {
        _anim.SetBool(_hashTrace, true);
    }

    public override TaskStatus OnUpdate()
    {  
        //falseKnight.transform.Translate()

        return TaskStatus.Success;
    }
}
