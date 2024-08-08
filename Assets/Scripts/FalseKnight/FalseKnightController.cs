using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightController : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _damage;

    private Rigidbody2D _rb;

    private Transform _playerTr;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    public bool CheckDistance(float range)
    { 
        if( _playerTr == null ) return false;

       float checkDist = Vector2.Distance(transform.position, _playerTr.position);

        if (checkDist <= range)
        {
            return true;
        }

        return false;
    }
}
