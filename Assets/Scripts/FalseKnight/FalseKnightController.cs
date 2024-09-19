using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightController : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _damage;
    [SerializeField] private float _waveDamage;
    [SerializeField] private float _hammerDamage;

    public bool isEndAttack = false;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private Transform _playerTr;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

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

    public void MoveFalseKnight(float speed)
    {
        if (transform.position.x - _playerTr.position.x > 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            _sr.flipX = true;
        }
        else if (transform.position.x - _playerTr.position.x < 0)
        { 
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            _sr.flipX = false; 
        }
    }

    private void SetAttackAnimationEvent()
    { 
        isEndAttack = true;
    }
}
