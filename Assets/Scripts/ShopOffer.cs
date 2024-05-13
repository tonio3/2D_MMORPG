using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOffer : MonoBehaviour
{
    [SerializeField] private ItemDatabaseSO _itemDatabase;

    [SerializeField] ShopItemSlot[] slots;
    public static ShopOffer Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Initialize();
    }


    public void Initialize()
    {
        foreach (var slot in slots)
        {
            if (slot.Item != null) continue;
            var a = _itemDatabase.GetRandomItem();
            slot.SetItem(a);
        }
    }
}
