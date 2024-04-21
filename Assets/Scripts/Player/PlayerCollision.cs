using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class PlayerCollision : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject hitCrack;
    public Animator hit;
    public AudioSource hitAudio;

    private int rocate;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            if(Keyboard.current.downArrowKey.IsPressed(0) == true) 
            {
                int bounce = transform.position.y - collision.transform.position.y > 0 ? 1 : -1;
                rb.AddForce(new Vector2(0, bounce * 10), ForceMode2D.Impulse);
                rb.gravityScale = 1.5f;
                StartCoroutine(HitCrack(collision.transform.position));
            }
            else
            {
                int bounce = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
                rb.AddForce(new Vector2(bounce * 3, 0), ForceMode2D.Impulse);
                StartCoroutine(HitCrack(collision.transform.position));
            }
        }
    }
    private IEnumerator HitCrack(Vector2 pos)
    {
        rocate = Random.Range(0, 6);
        hitCrack.SetActive(true);
        hitCrack.transform.position = pos;
        hitCrack.transform.rotation = Quaternion.Euler(0, 0, rocate * 30);
        hit.SetTrigger("Hit");
        hitAudio.Play(); 
        yield return new WaitForSeconds(0.5f);
        hitCrack.SetActive(false);
        rb.gravityScale = 1f;
    }
}
