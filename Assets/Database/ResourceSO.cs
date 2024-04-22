using System;

using UnityEngine;

[Flags]
public enum ResourceType
{
    Herb = 1 <<0,
    Leather = 1 << 1,
    Cloth = 1 <<2,
    Ingot = 1 << 3,
}

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource", order = 1)]
public class ResourceSO : ScriptableObject
{
    public Sprite Spr;
    public int Ammount = 0;
    public int Price = 0;
    [Space]
    public ResourceType resourceType;
}
