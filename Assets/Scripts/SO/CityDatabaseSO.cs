using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityDatabaseSO : ScriptableObject
{
    [SerializeField] CitySO[] cities;

    public Sprite GetRandomCity()
    {
        return cities[Random.Range(0, cities.Length)].MainSprite;
    }
}
