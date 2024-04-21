using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    public GameObject[] hpBar;
    public GameObject soulGage;
    public Animator animator;

    [SerializeField] private int maxHp = 4;
    [SerializeField] private int hp;
    [SerializeField] private int maxSoul = 4;

    public int soul;

    private bool canActive = true;


    void Start()
    {
        hp = maxHp;
        soul = 0;
    }

    void Update()
    {
        SettingHp();
        SettingSoul();
        MarkHp();
    }

    private void SettingHp()
    {
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        if (hp < 1)
        {
            Debug.Log("Die");
        }
    }
    private void SettingSoul()
    {
        if (soul > maxSoul)
        {
            soul = maxSoul;
        }
        else if (soul < 0)
        {
            soul = 0;
        }
    }
    private void MarkHp()
    {
        for (int i = 0; i < hpBar.Length; i++)
        {
            if (i < hp)
            {
                hpBar[i].gameObject.SetActive(true);
            }
            else
            {
                hpBar[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.performed && canActive == true)
        {
            if (context.interaction is HoldInteraction && soul >= 2)
            {
                StartCoroutine(Heal());
            }
            else if (context.interaction is PressInteraction && soul >= 1)
            {
                StartCoroutine(Skill());
            }
        }
    }
    private IEnumerator Heal()
    {
        canActive = false;
        animator.SetTrigger("Soul2Down");
        yield return new WaitForSeconds(1f);
        hp += 1;
        soul -= 2;
        canActive = true;
    }
    private IEnumerator Skill()
    {
        canActive = false;
        soul -= 1;
        animator.SetTrigger("SoulDown");
        yield return new WaitForSeconds(1f);
        canActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            hp -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fall Zone"))
        {
            gameObject.transform.position = new Vector2(65, 20);
        }
    }
}
