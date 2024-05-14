using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSlot : ItemSlot
{
     [SerializeField] private PlayerCurrencySO _playerCurrencySO;

    public override void SetItem(ItemSO item)
    {
        _item = item;
        _img.sprite = item.Spr;

        var invItem = new InventoryItem();
        invItem.Item = item;
  
        ShopOffer.Instance.Initialize();
    }

    public override void RemoveItem(ItemSO item)
    {
        _item = null;
        _img.sprite = null;

        var invItem = new InventoryItem();
        invItem.Item = item;
 
        ShopOffer.Instance.Initialize();
    }

    public override void OnDrop()
    {
        if (Item == null) return;

        RaycastHit2D hit = Physics2D.Raycast(_img.transform.position, Vector2.down);

        if (hit.collider != null && hit.collider.gameObject.tag == "ItemSlot")
        {

            var newSlot = hit.collider.gameObject.GetComponent<ItemSlot>();

            if (newSlot.Item != null) return;  

            //From shop to Empty inventory slot
            if (newSlot.SlotType == InventoryItemType)
            {
                if (!_playerCurrencySO.BuyForGold(Item.Price)) return;

                newSlot.SetItem(Item);
                RemoveItem(Item);
                ShopOffer.Instance.Initialize();
                return;
            }

            //From shop to Empty Gear slot
            if (newSlot.SlotType == Item.ItemType)
            {
                if (!_playerCurrencySO.BuyForGold(Item.Price)) return;

                newSlot.SetItem(Item);
                RemoveItem(Item);
                ShopOffer.Instance.Initialize();
                return;
            }


        }
    }
}
