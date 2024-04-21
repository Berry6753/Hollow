using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMController : MonoBehaviour
{
    public GameObject Cam;
    public GameObject place;
    void Awake()
    {
        Cam.SetActive(false);
        place.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        { 
            Cam.SetActive(true);
            place.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            Cam.SetActive(false);
            StartCoroutine(Place());
        }
    }

    private IEnumerator Place()
    {
        yield return new WaitForSeconds(1.5f);
        place.SetActive(false);
    }
}
