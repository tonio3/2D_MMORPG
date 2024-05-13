using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Unity.Services.CloudSave.Models;


public abstract class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField ] protected  Image _img;
    [SerializeField] protected  ItemSO _item;
    public ItemSO Item => _item;


    // -1 - Inventory 
    //  0 - Hat              // 4 - Necklace
    //  1 - Armor            // 5 - Gloves
    //  2 - Pants            // 6 - Ring
    //  3 - Shoes            // 7 - SpecialItem

    [Space]
    [SerializeField] protected ItemTypeSO slot;
    public ItemTypeSO SlotType =>slot;
    [SerializeField] protected ItemTypeSO ShopItemType;
    [SerializeField] protected ItemTypeSO InventoryItemType;
 
    int siblingsIndex;

    public abstract void SetItem(ItemSO item);

    public abstract void RemoveItem(ItemSO item); 

 
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item == null) return;
        siblingsIndex = _img.transform.GetSiblingIndex();
        _img.transform.SetParent(transform.parent.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Item == null) return;
        _img.transform.position = Input.mousePosition;
     
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item == null) return;

        OnDrop();

        _img.transform.position = transform.position;
        _img.transform.SetParent(transform);
        _img.transform.SetSiblingIndex(siblingsIndex);
    }

    public abstract void OnDrop();
   
}
