using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDatabaseSO : ScriptableObject
{

    [SerializeField] EnemySO[] enemies;

    public EnemySO GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }
}
