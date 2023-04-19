using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public LevelData levelData;

    private void Start()
    {
        // Получить случайный префаб уровня из списка
        //GameObject levelPrefab = levelData.levelPrefabs[Random.Range(0, levelData.levelPrefabs.Length)];
        //Instantiate(levelPrefab);

        // Создать врага на случайном вейпоинте спавна
        //GameObject enemyPrefab = levelData.enemyPrefabs[Random.Range(0, levelData.enemyPrefabs.Length)];
        //Transform spawnWaypoint = levelData.enemySpawnWaypoints[Random.Range(0, levelData.enemySpawnWaypoints.Length)];
        //Instantiate(enemyPrefab, spawnWaypoint.position, spawnWaypoint.rotation);
       
    }

}
