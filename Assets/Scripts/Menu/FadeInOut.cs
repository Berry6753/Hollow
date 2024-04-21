using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public GameObject intro;
    public GameObject menu;
    public Image panel;
    float time = 0f;
    float runTime = 1f;

    void Start()
    {
        intro.SetActive(true);
        StartCoroutine(StartDelayTime(3));
    }

    private IEnumerator StartDelayTime(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(StartDelay(1f));
    }

    private IEnumerator StartDelay(float time)
    {
        StartCoroutine(RunFade());
        yield return new WaitForSeconds(time);
        intro.SetActive(false);
        menu.SetActive(true);
    }

    public void Fade()
    {
        StartCoroutine(RunFade());
    }

    IEnumerator RunFade()
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        Color color = panel.color;
        while (color.a < 1f)
        {
            time += Time.deltaTime / runTime;
            color.a = Mathf.Lerp(0, 1, time);
            panel.color = color;
            yield return null;
        }
        time = 0f;

        yield return new WaitForSeconds(0.5f);

        while (color.a > 0f)
        {
            time += Time.deltaTime / runTime;
            color.a = Mathf.Lerp(1, 0, time);
            panel.color = color;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
}
