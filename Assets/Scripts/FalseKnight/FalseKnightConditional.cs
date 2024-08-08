using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightConditional : Conditional
{
    protected Rigidbody2D _rb;
    protected Animator _anim;

    public override void OnAwake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
}
