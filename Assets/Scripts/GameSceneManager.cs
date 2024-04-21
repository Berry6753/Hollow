using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void SceneChange()
    {
        StartCoroutine(DelayTime(1.5f));
    }
    private IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Main");
    }
}
