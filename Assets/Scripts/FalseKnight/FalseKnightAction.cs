using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightAction : Action
{
    public FalseKnightController falseKnight;
    protected Rigidbody2D _rb;
    protected Animator _anim;

    public override void OnAwake()
    {
        falseKnight = GetComponent<FalseKnightController>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
}
