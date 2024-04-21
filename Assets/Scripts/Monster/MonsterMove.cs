using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected Monster monster;

    public int nextMove;
    protected float nextThink;
    protected bool isDie = false;
    protected int hp;

    public int length;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monster = GetComponent<Monster>();
        Invoke("Think", nextThink);
    }

    private void Update()
    {
        Die();
    }
    private void FixedUpdate()
    {
        Move(length);
        StopDie();
    }
    protected void Move(int ray)
    {
        //기본 움직임
        rb.velocity = new Vector2(nextMove, rb.velocity.y);
        if (nextMove == -1)
        {
            spriteRenderer.flipX = false;
        }
        else if (nextMove == 1)
        {
            spriteRenderer.flipX = true;
        }

        //지형체크
        Vector2 frontVec = new Vector2(rb.position.x + nextMove * 0.5f, rb.position.y);
        Debug.DrawRay(frontVec, Vector2.down, new Color(0, ray, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, ray, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", nextThink);
        }
    }

    protected void Think()
    {
        nextMove = Random.Range(-1, 2);
        nextThink = Random.Range(2, 6);

        Invoke("Think", nextThink);
    }

    protected void StopDie()
    {
        hp = monster.hp;
        if (hp <= 0 )
        {
            rb.velocity = Vector2.zero;
            CancelInvoke();
        }
    }

    protected void Die()
    {
        hp = monster.hp;
        if (hp <= 0 && isDie == false)
        {
            StartCoroutine(DieMotion());
        }
    }
    protected IEnumerator DieMotion()
    {
        isDie = true;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(60f);
        isDie = false;
    }
}
