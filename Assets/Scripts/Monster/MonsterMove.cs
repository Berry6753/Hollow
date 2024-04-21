using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Monster monster;

    public int nextMove;
    private float nextThink;
    private int hp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monster = GetComponent<Monster>();
        Invoke("Think", nextThink);
    }

    private void FixedUpdate()
    {
        //�⺻ ������
        rb.velocity = new Vector2(nextMove, rb.velocity.y);
        if (nextMove == -1)
        {
            spriteRenderer.flipX = false;        
        }
        else if(nextMove == 1) 
        {
            spriteRenderer.flipX = true;
        }
        StopDie();

        //����üũ
        Vector2 frontVec = new Vector2(rb.position.x + nextMove*0.5f, rb.position.y);
        Debug.DrawRay(frontVec, Vector2.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", nextThink);
        }
    }

    private void Think()
    {
        nextMove = Random.Range(-1, 2);
        nextThink = Random.Range(2, 6);

        Invoke("Think", nextThink);
    }

    private void StopDie()
    { 
        hp = monster.hp;
        if (hp <= 0)
        {
            rb.velocity = Vector2.zero;
            CancelInvoke();
        }
    }
}
