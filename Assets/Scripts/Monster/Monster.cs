using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject monster;
    private Player player;

    public int hp;
    private int maxhp;
    private bool isDie = false;

    private void Awake()
    {
        maxhp = hp;
        player = FindObjectOfType<Player>();
        monster.SetActive(true);
    }

    private void OnEnable()
    {
        hp = maxhp;
        isDie = false;
    }

    private void Update()
    {
        if (hp <= 0 && isDie == false)
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(MarkDie());
    }
    private IEnumerator MarkDie()
    {
        isDie = true;
        gameObject.layer = 12;
        yield return new WaitForSeconds(60f);
        monster.SetActive(false);
        isDie = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            hp -= 1;
            player.soul++;
            player.animator.SetTrigger("SoulUp");
        }
        else if (collision.gameObject.CompareTag("Skill"))
        {
            hp -= 2;
        }
    }
}
