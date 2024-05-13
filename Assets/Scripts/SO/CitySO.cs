using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CitySO : ScriptableObject
{

    [field: SerializeField] public Sprite CityViewSprite { get; private set; }
    [field: SerializeField] public ResourceTypeSO[] CityResources { get; private set; }

}
