using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOffer : MonoBehaviour
{
    [SerializeField] DatabaseSO database;

    [SerializeField] ItemSlot[] slots;
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
            var a = database.GetRandomItem();
            slot.SetItem(a);
        }
    }
}
