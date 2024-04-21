using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskMove : MonsterMove
{
    public Rigidbody2D target;

    private float attackSpeed = 5f;
    private bool isDash = false;

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
        Move(4);
        if (nextMove == 0)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", true);
        }
        StopDie();
        Attack();
    }
    private void Attack()
    {
        Vector2 dirVec = target.position - rb.position;
        Collider2D cols = Physics2D.OverlapCircle(transform.position, 10f);
        if (cols.CompareTag("Player") && isDash == false)
        {
            CancelInvoke();
            animator.SetTrigger("Recog");
            animator.SetBool("Attack", true);
            StartCoroutine(Dash(dirVec));
            animator.SetBool("Attack", false);
            Invoke("Think", nextThink);
        }
    }
    private IEnumerator Dash(Vector2 pos)
    {
        isDash = true;
        if (pos.x > 0)
        {
            spriteRenderer.flipX = true;
            rb.velocity = new Vector2(transform.localScale.x * -attackSpeed, 0f);
        }
        else if (pos.x < 0)
        {
            spriteRenderer.flipX = false;
            rb.velocity = new Vector2(transform.localScale.x * attackSpeed, 0f);
        }
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        isDash = false;
    }
}
