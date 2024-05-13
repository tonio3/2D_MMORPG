using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
[System.Serializable]
public class ItemSO : ScriptableObjectWithIcon
{
 
    public int Price;
    public int Health;
    public int Damage;
    public ItemTypeSO ItemType;
 
}
