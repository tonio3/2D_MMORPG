using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
[System.Serializable]
public class GearItemSO : ItemSO 
{
 
    public int Health;
    public int Damage;
 
}
