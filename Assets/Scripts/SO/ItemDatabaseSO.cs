using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDatabaseSO : ScriptableObject
{

    [SerializeField] ItemSOArray[] items;

    public ItemSO GetItem(string type, string id)
    {
        foreach (var itemArray in items)
        {
            if (itemArray.Type.name == type)
            {
                foreach (var item in itemArray.Items)
                {
                    if (item.name == id)
                    {
                        return item;
                    }
                }
            }
        }
        return null; // Pokud položka není nalezena
    }

    public ItemSO GetRandomItem()
    {
        var randomIndex = Random.Range(0, items.Length);
        var randomIndex2 = Random.Range(0, items[randomIndex].Items.Length);

        var randomItem = items[randomIndex].Items[randomIndex2];

        return randomItem;
    }

    [Serializable]
    private class ItemSOArray
    {
        [SerializeField] string Name;
        public ItemTypeSO Type;
        public ItemSO[] Items;
    }
}
