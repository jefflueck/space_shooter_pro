﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private bool _stopSpawning = false;

    public void StartSpawing()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(3.0f);
        // every 3 -7 seconds, spawn in a powerup
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int powerupIndex = Random.Range(0, 3);
            Instantiate(powerups[powerupIndex], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));


        }

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
