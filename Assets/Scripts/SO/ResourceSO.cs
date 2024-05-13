using System;
using UnityEngine;
 
[System.Serializable]
[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource", order = 1)]
public class ResourceSO : ScriptableObjectWithIcon
{
    public int Ammount = 0;
    public int Price = 0;
    [Space]
    public ResourceTypeSO _resourceTypeSO;
}
