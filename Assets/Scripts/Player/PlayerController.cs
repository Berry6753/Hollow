using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpSpeed = 10f;
    private float dashSpeed = 25f;
    private float dashTime = 0.2f;
    private float dashCooldown = 0.5f;
    private float attackTime = 0.5f;

    private Vector2 inputMovement = Vector2.zero;
    private Vector2 inputJump = Vector2.zero;

    private Rigidbody2D playerRigid;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Player player;

    public GameObject hitCrack;
    public Animator hit;
    public AudioSource[] actAudio;  //0. 대쉬 1. 점프 2. 이동 3. 피해

    public bool isJump = false;
    private bool isRun = false;
    private bool hasJump = false;
    private bool canDash = true;
    private bool isSlash = false;
    private bool isSkill = false;
    private bool isHeal = false;
    private bool isActive = false;

    private int soul;

    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();

    }

    private void Update()
    {
        soul = player.soul;
    }
    private void FixedUpdate()
    {
        Vector2 move = inputMovement * moveSpeed * Time.deltaTime;
        transform.Translate(move);
        Vector2 jump = inputJump * jumpSpeed * Time.deltaTime;
        transform.Translate(jump);

        Debug.DrawRay(transform.position, Vector2.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider != null)
        {
            if (rayHit.distance < 1f)
            {
                animator.SetTrigger("Land");
                //Debug.Log(rayHit.collider.name);
                isJump = false;
                hasJump = false;
                playerRigid.gravityScale = 1f;
                animator.SetBool("Jump", isJump);
            }
        }
        else if (rayHit.collider == null)
        {
            isJump = true;
            hasJump = true;
            animator.SetBool("Jump", isJump);
            if(inputMovement != Vector2.zero)
            {
                actAudio[2].Play();
            }
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)    //send massage일 경우 InputValue inputvalue
    {
        inputMovement = context.ReadValue<Vector2>();               //ReadValue 대신에 Get
        if (inputMovement == Vector2.zero)
        {
            animator.SetTrigger("RunEnd");
            animator.SetBool("Run", isRun);
            actAudio[2].Stop();
        }
        else
        {
            animator.SetTrigger("RunStart");
            animator.SetBool("Run", !isRun);
            if (inputMovement.x < 0f)
            {
                spriteRenderer.flipX = false;
            }
            else if (inputMovement.x > 0f)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (inputJump == Vector2.zero && hasJump == true)
        {

        }
        else
        {
            inputJump = context.ReadValue<Vector2>();
            if (context.started)
            {
                actAudio[1].Play();
            }
        }
    }

    public void OnSlash(InputAction.CallbackContext context)
    {
        if (context.started && isSlash == false && isActive == false)
        {
            if (Keyboard.current.upArrowKey.isPressed == true)
            {
                animator.SetTrigger("UpSlash");
                StartCoroutine(Slash());
            }
            else if (Keyboard.current.downArrowKey.isPressed == true && isJump == true)
            {
                animator.SetTrigger("DownSlash");
                StartCoroutine(Slash());
            }
            else
            {
                animator.SetTrigger("Slash");
                StartCoroutine(Slash());
            }
        }
    }
    private IEnumerator Slash()
    {
        isSlash = true;
        isActive = true;
        yield return new WaitForSeconds(attackTime);
        isActive = false;
        isSlash = false;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && canDash)
        {
            animator.SetTrigger("Dash");
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        actAudio[0].Play();
        inputJump = Vector2.zero;
        //float originalGravity = playerRigid.gravityScale;
        playerRigid.gravityScale = 0f;
        if (spriteRenderer.flipX == false)
        {
            playerRigid.velocity = new Vector2(transform.localScale.x * -dashSpeed, 0f);
        }
        else if (spriteRenderer.flipX == true)
        {
            playerRigid.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        }
        yield return new WaitForSeconds(dashTime);
        playerRigid.velocity = Vector2.zero;
        playerRigid.gravityScale = 1.5f;        //원래 중력으로 돌릴라면 originalGravity
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.performed && isActive == false)
        {
            if (context.interaction is HoldInteraction && isHeal == false && canDash == true && soul >= 2)
            {
                animator.SetTrigger("Heal");
                StartCoroutine(Heal());
            }
            else if (context.interaction is PressInteraction && isSkill == false && soul >= 1)
            {
                animator.SetTrigger("Skill");
                StartCoroutine(Skill());
            }
        }
    }
    private IEnumerator Heal()
    { 
        isHeal = true;
        isActive = true;
        canDash = false;
        inputMovement = Vector2.zero;
        inputJump = Vector2.zero;
        yield return new WaitForSeconds(1f);
        canDash = true;
        isActive = false;
        isHeal = false;
    }
    private IEnumerator Skill()
    {
        isSkill = true;
        isActive = true;
        inputMovement = Vector2.zero;
        inputJump = Vector2.zero;
        yield return new WaitForSeconds(1f);
        isActive = false;
        isSkill = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && gameObject.layer == 10)
        {
            StartCoroutine(Attacked(collision.transform.position));
        }
    }

    private void OnDamaged(Vector2 pos)
    {
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        inputMovement = Vector2.zero;
        int bounce = transform.position.x - pos.x > 0 ? 1 : -1;
        playerRigid.AddForce(new Vector2(bounce*2, 0), ForceMode2D.Impulse);
    }

    private void OffDagamed()
    {
        gameObject.layer = 10;

        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    private IEnumerator Attacked(Vector2 pos)
    { 
        OnDamaged(pos);
        hitCrack.SetActive(true);
        hit.SetTrigger("Hit");
        actAudio[3].Play();
        yield return new WaitForSeconds(0.5f);
        hitCrack.SetActive(false);
        OffDagamed();
    }
}
