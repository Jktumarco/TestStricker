using System;
using UnityEngine;
[Serializable]
public class Level
{
    [SerializeField] private int nameLevel;
    [SerializeField] Platform[] platformsList;
    [Header("Level Prefab")]
    [SerializeField] private GameObject levelPrefab;
    [Header("Level PointsEnemy")]
    [SerializeField] private Transform[] enemySpawnWaypoints;
    [Header("Level WayPointCharacter")]
    [SerializeField] private Transform[] wayPointsForCharacter;

    public GameObject LevelPrefab { get => levelPrefab; }
    public int CountLevelName { get => nameLevel; }
    public Transform[] EnemySpawnWaypoints { get => enemySpawnWaypoints; }
    public Platform[] PlatformsList { get => platformsList; }

    public Transform GetWayPoint(int curPoint) { Debug.Log(curPoint); return wayPointsForCharacter[curPoint]; }

    public void KillEnemy(int platform)
    {
        PlatformsList[platform].EnemyAmount--;
    }
    public bool AllEnemyIsDied(int platform)
    {
        if (PlatformsList[platform].EnemyAmount == 0)
        {
            return true;
        }
        else return false;
    }
}
