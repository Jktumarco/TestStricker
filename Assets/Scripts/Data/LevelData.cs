using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Custom/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private List<Level> listLevels;


    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;

    public GameObject[] EnemyPrefabs { get => enemyPrefabs; }
    public List<Level> ListLevels { get => listLevels;  }

    public GameObject enemyPrefab;
    public Transform GetWay(int curLevel, int curWayPoint) { return listLevels[curLevel].GetWayPoint(curWayPoint); }
   

}
