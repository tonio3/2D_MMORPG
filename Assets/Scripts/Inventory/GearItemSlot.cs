using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearItemSlot : ItemSlot
{
    public static Action<ItemSO> OnItemSetToGear
    {
        get; set;
    }

    public static Action<ItemSO> OnItemRemovedFromGear
    {
        get; set;
    }

    public override void SetItem(ItemSO item)
    {
        _item = item;
        _img.sprite = item.Spr;
        OnItemSetToGear?.Invoke(item);
    }

    public override void RemoveItem(ItemSO item)
    {
        _item = null;
        _img.sprite = null;

        OnItemRemovedFromGear?.Invoke(item);
    }

    public override void OnDrop()
    {
        if (Item == null) return;

        RaycastHit2D hit = Physics2D.Raycast(_img.transform.position, Vector2.down);

        if (hit.collider != null && hit.collider.gameObject.tag == "ItemSlot")
        {

            var newSlot = hit.collider.gameObject.GetComponent<ItemSlot>();

            if (slot == ShopItemType && newSlot.Item != null) return;
            if (newSlot.SlotType == ShopItemType && slot == ShopItemType) return;

            //From Gear slot to shop
            if ( newSlot.SlotType == ShopItemType)
            {
                Player.Instance.Sell(Item.Price);
                RemoveItem(Item);
                ShopOffer.Instance.Initialize();
                return;
            }

            //From Gear slot to Empty Inventory slot
            if ( newSlot.SlotType == InventoryItemType && newSlot.Item == null)
            {
                newSlot.SetItem(Item);
                RemoveItem(Item);
                return;
            }
 

        }
    }
}
