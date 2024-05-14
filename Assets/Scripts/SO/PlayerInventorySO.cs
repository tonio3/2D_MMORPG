using System;
using System.Threading.Tasks;
using Unity.Services.Economy;
using UnityEngine;

[Serializable]
public class PlayerInventorySO : ScriptableObject
{

    [SerializeField] private InventoryItem[] _itemSlots = new InventoryItem[6];

    [SerializeField] private GearItem[] _gearSlots = new GearItem[8];

    [SerializeField] private ItemDatabaseSO _itemDatabaseSO;
    // -1 - Inventory 
    //  0 - Hat              // 4 - Necklace
    //  1 - Armor            // 5 - Gloves
    //  2 - Pants            // 6 - Ring
    //  3 - Shoes            // 7 - SpecialItem

    public InventoryItem[] ItemSlots => _itemSlots;

    public GearItem[] GearSlots => _gearSlots;

    public event Action<InventoryItem> OnItemAddedToInventorySlot;

    
    public event Action<ItemSO> OnItemAddedToGearSlot;

 
    public event Action<InventoryItem> OnItemRemovedFromSlot;

 
    public event Action<ItemSO> OnItemRemovedFromGearSlot;

    private void OnEnable()
    {
        SubscribeToItemSlotEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromItemSlotEvents();

        foreach (var slot in _itemSlots)
        {
            slot.Item = null;
        }

        foreach (var slot in _gearSlots)
        {
            slot.Item = null;
        }


    }

    private void SubscribeToItemSlotEvents()
    {
        InventoryItemSlot.OnItemSetToInventory += AddItemToInventorySlot;
        InventoryItemSlot.OnItemRemovedFromInventory += RemoveItemFromInventorySlot;
        GearItemSlot.OnItemRemovedFromGear += RemoveItemFromGearSlot;
        GearItemSlot.OnItemSetToGear += AddItemToGearSlot;

    }

    private void UnsubscribeFromItemSlotEvents()
    {
        InventoryItemSlot.OnItemSetToInventory -= AddItemToInventorySlot;
        InventoryItemSlot.OnItemRemovedFromInventory -= RemoveItemFromInventorySlot;
        GearItemSlot.OnItemRemovedFromGear -= RemoveItemFromGearSlot;
        GearItemSlot.OnItemSetToGear -= AddItemToGearSlot;
    }

    public void AddItemToInventorySlot(InventoryItem itemSlot)
    {
        _itemSlots[itemSlot.InventorySlotId].Item = itemSlot.Item;
        OnItemAddedToInventorySlot?.Invoke(itemSlot);  
    }

    public void AddItemToGearSlot(ItemSO item)
    {
        foreach (var itemArray in _gearSlots)
        {
            if (itemArray.GearSlotType == item.ItemType)
            {
                itemArray.Item =  item;
            }
        }

        OnItemAddedToGearSlot?.Invoke(item);  
    }

    private void RemoveItemFromInventorySlot(InventoryItem itemSlot)
    {
        _itemSlots[itemSlot.InventorySlotId].Item = null;
        OnItemRemovedFromSlot?.Invoke(itemSlot);
    }


    private void RemoveItemFromGearSlot(ItemSO item)
    {
        foreach (var itemArray in _gearSlots)
        {
            if (itemArray.GearSlotType == item.ItemType)
            {
                itemArray.Item = null;
            }
        }

        OnItemRemovedFromGearSlot?.Invoke(item);  
    }

    public async Task LoadInventoryFromCloud()
    {
        var inventory = await EconomyService.Instance.PlayerInventory.GetInventoryAsync();

        foreach (var item in inventory.PlayersInventoryItems)
        {
            // přístup k datům každé položky
            var instancedata = item.InstanceData;

            var itemdata = JsonUtility.FromJson<ItemDTO>(instancedata.GetAsString());
            if (itemdata == null) 
            {

                continue;
            } 

            if (item.InventoryItemId.Contains("GEAR"))
            {
                AddItemToGearSlot(_itemDatabaseSO.GetItem(itemdata.ItemType, itemdata.ItemName));
            }

            else if (item.InventoryItemId.Contains("INV"))
            {
                var v = new InventoryItem();
                v.Item = _itemDatabaseSO.GetItem(itemdata.ItemType, itemdata.ItemName);
                v.InventorySlotId = itemdata.InventorySlotId;
                AddItemToInventorySlot(v);
            }
        }
 
    }
}
