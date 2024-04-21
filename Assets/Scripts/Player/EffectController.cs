using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class EffectController : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public GameObject[] effect;
    //0. 대쉬, 1. 다운슬래쉬, 2. 스킬1, 3. 슬래쉬, 4. 업슬래쉬 5. 힐 6. 스킬2
    public SpriteRenderer[] effectSprite;
    //0. 대쉬, 1. 다운슬래쉬, 2. 스킬1, 3. 슬래쉬, 4. 업슬래쉬 5. 힐 6. 스킬2
    public Animator[] animator;
    //0. 대쉬, 1. 다운슬래쉬, 2. 스킬1, 3. 슬래쉬, 4. 업슬래쉬 5. 힐 6. 스킬2

    public Collider2D[] colliders;
    //0. 슬래쉬, 1. 스킬1

    private Player player;
    private PlayerController playerController;
    private int soul;

    private bool canJump = true;
    private bool canDash = true;
    private bool canSlash = true;
    private bool canSkill = true;
    private bool canHeal = true;
    private bool canActive = true;

    private float skillSpeed = 20f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        soul = player.soul;
        canJump = !playerController.isJump;
    }
    public void DashEffect(InputAction.CallbackContext context)
    {
        if (context.started && canDash)
        {
            effect[0].SetActive(true);
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        Vector2 save = effect[0].transform.localPosition;
        if (playerSprite.flipX == false)
        {
            effectSprite[0].flipX = true;
        }
        else if (playerSprite.flipX == true)
        {
            effect[0].transform.Translate(Vector2.left * 4f, Space.Self);
            effectSprite[0].flipX = false;
        }
        animator[0].SetTrigger("DashEffect");
        yield return new WaitForSeconds(0.2f);
        effect[0].SetActive(false);
        effect[0].transform.localPosition = save;
        yield return new WaitForSeconds(0.5f);
        canDash = true;
    }

    public void SlashEffect(InputAction.CallbackContext context)
    {
        if (context.started && canSlash && canActive)
        {
            if (Keyboard.current.upArrowKey.isPressed == true)
            {
                effect[4].SetActive(true);
                StartCoroutine(UDSlash(4, "UpslashEffect"));
            }
            else if (Keyboard.current.downArrowKey.isPressed == true && canJump == false)
            {
                effect[1].SetActive(true);
                StartCoroutine(UDSlash(1, "DownslashEffect"));
            }
            else
            {
                effect[3].SetActive(true);
                StartCoroutine(Slash());
            }
        }
    }
    private IEnumerator Slash()
    {
        canSlash = false;
        canActive = false;
        float saveX = colliders[0].offset.x;
        float saveY = colliders[0].offset.y;
        if (playerSprite.flipX == false)
        {
            effectSprite[3].flipX = false;
        }
        else if (playerSprite.flipX == true)
        {
            colliders[0].offset = new Vector2(-saveX, saveY);
            effectSprite[3].flipX = true;
        }
        animator[3].SetTrigger("SlashEffect");
        yield return new WaitForSeconds(0.5f);
        effect[3].SetActive(false);
        colliders[0].offset = new Vector2(saveX, saveY);
        canActive = true;
        canSlash = true;
    }
    private IEnumerator UDSlash(int index, string effectS)
    {
        canSlash = false;
        canActive = false;
        if (playerSprite.flipX == false)
        {
            effectSprite[index].flipX = false;
        }
        else if (playerSprite.flipX == true)
        {
            effectSprite[index].flipX = true;
        }
        animator[index].SetTrigger(effectS);
        yield return new WaitForSeconds(0.5f);
        effect[index].SetActive(false);
        canActive = true;
        canSlash = true;
    }

    public void SkillEffect(InputAction.CallbackContext context)
    {
        if (context.performed && canActive)
        {
            if (context.interaction is HoldInteraction && canHeal == true && canDash && soul >= 2)
            {
                effect[5].SetActive(true);
                StartCoroutine(Heal());
            }
            else if (context.interaction is PressInteraction && canSkill == true && soul >= 1)
            {
                effect[2].SetActive(true);
                effect[6].SetActive(true);
                StartCoroutine(Skill());
            }
        }
    }
    private IEnumerator Heal()
    {
        canHeal = false;
        canActive = false;
        canDash = false;
        animator[5].SetTrigger("HealEffect");
        yield return new WaitForSeconds(1f);
        effect[5].SetActive(false);
        canDash = true;
        canActive = true;
        canHeal = true;
    }
    private IEnumerator Skill()
    {
        canSkill = false;
        canActive = false;
        Rigidbody2D skillRigid = effect[2].GetComponent<Rigidbody2D>();
        if (playerSprite.flipX == false)
        {
            effectSprite[2].flipX = true;
            effectSprite[6].flipX = false;
            skillRigid.AddForce(Vector2.left * skillSpeed, ForceMode2D.Impulse);
        }
        else if (playerSprite.flipX == true)
        {
            effectSprite[2].flipX = false;
            effectSprite[6].flipX = true;
            skillRigid.AddForce(Vector2.right * skillSpeed, ForceMode2D.Impulse);
        }
        animator[2].SetTrigger("SkillEffect");
        animator[6].SetTrigger("Skill2Effect");
        yield return new WaitForSeconds(0.5f);
        effect[6].SetActive(false);
        effect[2].SetActive(false);
        Vector2 save = effect[6].transform.position;
        effect[2].transform.position = save;
        yield return new WaitForSeconds(0.5f);
        canActive = true;
        canSkill = true;
    }
}
