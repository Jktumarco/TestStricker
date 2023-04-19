using UnityEngine;
using System;
[Serializable]
public class Platform 
{
    [SerializeField] Transform wayPoint;
    [SerializeField] int enemyAmount;
    private bool noEnemy = false;
    public int EnemyAmount {
        get {
            if (enemyAmount == 0) 
            { 
                noEnemy = true; 
            };
            return enemyAmount; } 
        set => enemyAmount = value;
    }

    public bool NoEnemy { get => noEnemy; }
    public Transform WayPoint { get => wayPoint; }
}
