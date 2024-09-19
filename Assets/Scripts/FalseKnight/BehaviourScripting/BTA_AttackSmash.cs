using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_AttackSmash : FalseKnightAction
{
    [SerializeField] private int _smashPattern;

    private readonly int _hashAttack = Animator.StringToHash("Attack");
    private readonly int _hashPattern = Animator.StringToHash("Pattern");

    public override void OnStart()
    {
        _anim.SetTrigger(_hashAttack);
        _anim.SetInteger(_hashPattern, _smashPattern);
    }

    public override TaskStatus OnUpdate()
    {
        if (falseKnight.isEndAttack)
        {
            falseKnight.isEndAttack = true;
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
