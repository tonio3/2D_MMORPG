using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Helmet,
    Armor,
    Gloves,
    Boots,
    Weapon,
    Shield,
    Ring,
    Necklace,
    SpecialItem,
    Consumable,
    Shop,
    Inventory,
    Resources,
}

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public Sprite Spr;
    public int Price;

    public int Ammount;

    public int Hp;
    public int AttackDamage;
    [Space]
    public ItemType Type;

}
