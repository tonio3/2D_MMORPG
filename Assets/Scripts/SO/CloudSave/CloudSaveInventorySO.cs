using System.Collections;
using System.Collections.Generic;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu]
public class CloudSaveInventorySO : ScriptableObject
{
    private void OnEnable()
    {
        SubscribeToPlayerEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromPlayerEvents();
    }

    private void SubscribeToPlayerEvents()
    {
        InventoryItemSlot.OnItemSetToInventory += SaveInventoryItemToCloud;
        GearItemSlot.OnItemSetToGear += SaveGearItemToCloud;
        InventoryItemSlot.OnItemRemovedFromInventory += removeItemFromInv;
        GearItemSlot.OnItemRemovedFromGear += RemoveItemFromGear;
    }

    private void UnsubscribeFromPlayerEvents()
    {
        InventoryItemSlot.OnItemSetToInventory -= SaveInventoryItemToCloud;
        GearItemSlot.OnItemSetToGear -= SaveGearItemToCloud;
        InventoryItemSlot.OnItemRemovedFromInventory -= removeItemFromInv;
        GearItemSlot.OnItemRemovedFromGear -= RemoveItemFromGear;
    }

    private async void SaveGearItemToCloud(ItemSO item)
    {  
        if (item != null)
        {


            Dictionary<string, object> instanceData = new Dictionary<string, object>
            {
               { "ItemName", item.name},
               { "ItemType", item.ItemType.name},         
            };

            UpdatePlayersInventoryItemOptions options = new UpdatePlayersInventoryItemOptions
            {
                WriteLock = null
            };

            await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync("GearSlotId_" + item.ItemType.name, instanceData, options);

        } 
    }

    private async void SaveInventoryItemToCloud(InventoryItem item)
    { 
        if (item != null)
        {

            Dictionary<string, object> instanceData = new Dictionary<string, object>
            {
               { "ItemName", item.Item.name},
               { "ItemType", item.Item.ItemType.name},
               { "InventorySlotId", item.InventorySlotId}
            };

            UpdatePlayersInventoryItemOptions options = new UpdatePlayersInventoryItemOptions
            {
                WriteLock = null
            };

            await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync("InventorySlotId_" + item.InventorySlotId, instanceData, options);

        } 
    }

    private async void removeItemFromInv(InventoryItem item)
    {
         
        UpdatePlayersInventoryItemOptions options = new UpdatePlayersInventoryItemOptions
        {
            WriteLock = null
        };

        await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync("InventorySlotId_" + item.InventorySlotId, null, options);
    }


    private async void RemoveItemFromGear(ItemSO item)
    {
 
        UpdatePlayersInventoryItemOptions options = new UpdatePlayersInventoryItemOptions
        {
            WriteLock = null
        };

        await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync("GearSlotId_" + item.ItemType.name, null, options);
    }
 

}
