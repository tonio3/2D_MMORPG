using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
[CreateAssetMenu(fileName = "Database", menuName = "ScriptableObjects/Database", order = 1)]
public class DatabaseSO : ScriptableObject
{

    [SerializeField] ItemSO[] items;
    [SerializeField] EnemySO[] enemies;
    [SerializeField] ResourceSO[] resources;
    public Town[] countries;

    public static DatabaseSO dSO;

    private void OnValidate()
    {
        dSO = this;

        foreach (var item in countries)
        {
            item.OnValidate();
        }
    }

    public ItemSO GetRandomItem()
    {
        return items[Random.Range(0, items.Length)];
    }

    public ResourceSO GetRandomResource(ResourceType rt)
    {
        List<ResourceSO> rl = new List<ResourceSO>();

        foreach (ResourceSO r in resources)
        {
            r.name = r.Spr.name;
            if ((rt & r.resourceType) != 0)
            {
               
                rl.Add(r);
            }
        }

        return rl[Random.Range(0, rl.Count)];
    }

    public EnemySO GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }
}
