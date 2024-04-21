using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    public GameObject[] button;

    public void ChangeSettings()
    {
        StartCoroutine(SettingDelay(1.5f));
    }

    private IEnumerator SettingDelay(float time)
    {
        yield return new WaitForSeconds(time);
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void BackMenu()
    {
        StartCoroutine(BackDelay(1.5f));
    }

    private IEnumerator BackDelay(float time)
    {
        yield return new WaitForSeconds(time);
        settings.SetActive(false);
        menu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();   //어플 종료
    }
}
