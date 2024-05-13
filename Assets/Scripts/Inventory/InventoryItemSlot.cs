using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSlot : ItemSlot
{


    [SerializeField] protected int _slotId;
    public int SlotId => _slotId;

    public static Action<InventoryItem> OnItemSetToInventory
    {
        get; set;
    }

    public static Action<InventoryItem> OnItemRemovedFromInventory
    {
        get; set;
    }

    public override void SetItem(ItemSO item)
    {
        _item = item;
        _img.sprite = item.Spr;

        var invItem = new InventoryItem();
        invItem.Item = item;
        invItem.InventorySlotId = SlotId;

        OnItemSetToInventory?.Invoke(invItem);
    }

    public override void RemoveItem(ItemSO item)
    {
        _item = null;
        _img.sprite = null;

        var invItem = new InventoryItem();
        invItem.Item = item;
        invItem.InventorySlotId = SlotId;

        OnItemRemovedFromInventory?.Invoke(invItem);
    }

    public override void OnDrop()
    {
        if (Item == null) return;

        RaycastHit2D hit = Physics2D.Raycast(_img.transform.position, Vector2.down);

        if (hit.collider != null && hit.collider.gameObject.tag == "ItemSlot")
        {

            var newSlot = hit.collider.gameObject.GetComponent<ItemSlot>();

            //From Inventory slot to Full Inventory slot - Switch
            if (newSlot.Item != null && slot == InventoryItemType && newSlot.SlotType == InventoryItemType)
            {
                var _item = Item;
                var _newSlotItem = newSlot.Item;

                SetItem(_newSlotItem);
                newSlot.SetItem(_item);
                return;
            }

            //From Inventory slot to empty gear slot
            if (newSlot.SlotType == Item.ItemType && newSlot.Item == null)
            {
                newSlot.SetItem(Item);
                RemoveItem(Item);
                return;
            }

            //From Inventory slot to full gear slot - Switch
            if (newSlot.SlotType == Item.ItemType && newSlot.Item != null)
            {
                var _item = Item;
                var _newSlotItem = newSlot.Item;

                SetItem(Item);
                newSlot.SetItem(Item);
                return;
            }

            //From Inventory slot to empty inventory slot
            if (newSlot.SlotType == InventoryItemType && newSlot.Item == null)
            {
                newSlot.SetItem(Item);
                RemoveItem(Item);
                return;
            }

            //From Inventory slot to shop
            if (newSlot.SlotType == ShopItemType)
            {
                Player.Instance.Sell(Item.Price);
                RemoveItem(Item);
                ShopOffer.Instance.Initialize();
                return;
            }


        }
    }
}
