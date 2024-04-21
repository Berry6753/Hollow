using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int monsterNum;

    public Transform[] spawnPoint;

    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 20f)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject sMosnter = GameManager.Instance.pool.Get(monsterNum);
        sMosnter.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}

