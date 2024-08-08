using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightController : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;

    private Transform _playerTr;

    private void Awake()
    {
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
