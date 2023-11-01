using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemySet;

    public void Start()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnEnemy(i,i);
        }

    }

    public void SpawnEnemy(int enemyNum, int enemyPoint)
    {
        Instantiate(enemySet[enemyNum], spawnPoints[enemyPoint]);
    }
}
