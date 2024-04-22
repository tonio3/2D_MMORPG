using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
 

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemSO Item;
    [SerializeField] Image img;
    int siblingsIndex;
    public ItemType slot;
 
    public void SetItem(ItemSO item)
    {
        Item = item;
        img.sprite = item.Spr;   
    }

    public void RemoveItem(ItemSO item)
    {
        Item = null;
        img.sprite = null;    
    }

    public void SetItemToGear(ItemSO item)
    {
        Item = item;
        img.sprite = item.Spr;
        Player.Instance.SetGearItem(item);
    }

    public void RemoveItemFromGear(ItemSO item)
    {
        Item = null;
        img.sprite = null;
        Player.Instance.RemoveGearItem(item);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item == null) return;
        siblingsIndex = img.transform.GetSiblingIndex();
        img.transform.SetParent(transform.parent.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Item == null) return;
        img.transform.position = Input.mousePosition;
     
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item == null) return;

        OnDrop();

        img.transform.position = transform.position;
        img.transform.SetParent(transform);
        img.transform.SetSiblingIndex(siblingsIndex);
    }

    private void OnDrop()
    {
        if (Item == null) return;

        RaycastHit2D hit = Physics2D.Raycast(img.transform.position, Vector2.down);
        
        if (hit.collider != null && hit.collider.gameObject.tag == "ItemSlot")
        {

            var newSlot = hit.collider.gameObject.GetComponent<ItemSlot>();

            if (slot == ItemType.Shop && newSlot.Item != null) return;
            if (newSlot.slot == ItemType.Shop && slot == ItemType.Shop)  return;


            //From shop to Empty inventory slot
            if (slot == ItemType.Shop && newSlot.slot == ItemType.Inventory && newSlot.Item == null)
            {
                if (!Player.Instance.CanBuy(Item.Price)) return;

                newSlot.SetItem(Item);
                RemoveItem(Item);
                ShopOffer.Instance.Initialize();
                return;
            }

            //From shop to Empty Gear slot
            if (slot == ItemType.Shop && newSlot.slot == Item.Type && newSlot.Item == null)
            {
                if (!Player.Instance.CanBuy(Item.Price)) return;

                newSlot.SetItemToGear(Item);
                RemoveItem(Item);
                ShopOffer.Instance.Initialize();
                return;
            }

            //From Inventory slot to Full Inventory slot - Switch
            if (newSlot.Item != null && slot == ItemType.Inventory && newSlot.slot == ItemType.Inventory)
            {
                var _item = Item;
                var _newSlotItem = newSlot.Item;

                SetItem(_newSlotItem);
                newSlot.SetItem(_item);
                return;
            }

            //From Inventory slot to empty gear slot
            if (slot == ItemType.Inventory && newSlot.slot == Item.Type && newSlot.Item == null)
            {
                newSlot.SetItemToGear(Item);
                RemoveItem(Item);

                return;
            }

            //From Inventory slot to full gear slot - Switch
            if (slot == ItemType.Inventory && newSlot.slot == Item.Type && newSlot.Item != null)
            {
                var _item = Item;
                var _newSlotItem = newSlot.Item;

                SetItemToGear(_newSlotItem);
                newSlot.SetItemToGear(_item);
                return;
            }

            //From Inventory slot to empty inventory slot
            if (slot == ItemType.Inventory && newSlot.slot == ItemType.Inventory && newSlot.Item == null)
            {
                newSlot.SetItem(Item);
                RemoveItem(Item);
                return;
            }

            //From Gear slot to shop
            if (slot == Item.Type && newSlot.slot == ItemType.Shop)
            {
                Player.Instance.Sell(Item.Price);
                RemoveItemFromGear(Item);
                ShopOffer.Instance.Initialize();
                return;
            }

            //From Gear slot to Empty Inventory slot
            if (slot == Item.Type && newSlot.slot == ItemType.Inventory && newSlot.Item == null)
            {
                newSlot.SetItem(Item);
                RemoveItemFromGear(Item);
                return;
            }

            //From Inventory slot to shop
            if (slot == ItemType.Inventory && newSlot.slot == ItemType.Shop)
            {          
                Player.Instance.Sell((int)(Item.Price * 0.75f));
                RemoveItem(Item);
                ShopOffer.Instance.Initialize();
                return;
            }


        }

    }
}
