using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

[CreateAssetMenu]
public class CloudSaveInventorySO : CloudSaveInitializable
{
 
    // Event subscription methods
    protected override void SubscribeToPlayerEvents()
    {
        InventoryItemSlot.OnItemSetToInventory += SaveInventoryItemToCloud;
        GearItemSlot.OnItemSetToGear += SaveGearItemToCloud;
        InventoryItemSlot.OnItemRemovedFromInventory += RemoveItemFromInventory;
        GearItemSlot.OnItemRemovedFromGear += RemoveItemFromGear;
    }

    protected override void UnsubscribeFromPlayerEvents()
    {
        InventoryItemSlot.OnItemSetToInventory -= SaveInventoryItemToCloud;
        GearItemSlot.OnItemSetToGear -= SaveGearItemToCloud;
        InventoryItemSlot.OnItemRemovedFromInventory -= RemoveItemFromInventory;
        GearItemSlot.OnItemRemovedFromGear -= RemoveItemFromGear;
    }

    // Event handler methods
    private async void SaveInventoryItemToCloud(InventoryItem item)
    {
        await SaveInventoryItemToCloudTask(item);
    }

    private async void SaveGearItemToCloud(ItemSO item)
    {
        await SaveGearItemToCloudTask(item);
    }

    private async void RemoveItemFromInventory(InventoryItem item)
    {
        await RemoveItemFromInventoryTask(item);
    }

    private async void RemoveItemFromGear(ItemSO item)
    {
        await RemoveItemFromGearTask(item);
    }

    // Task methods
    private async Task SaveInventoryItemToCloudTask(InventoryItem item)
    {
        if (item != null)
        {
            var instanceData = new Dictionary<string, object>
            {
                { "ItemName", item.Item.name },
                { "ItemType", item.Item.ItemType.name },
                { "InventorySlotId", item.InventorySlotId }
            };

            var options = new UpdatePlayersInventoryItemOptions { WriteLock = null };
            await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync("InventorySlotId_" + item.InventorySlotId, instanceData, options);
        }
    }

    private async Task SaveGearItemToCloudTask(ItemSO item)
    {
        if (item != null)
        {
            var instanceData = new Dictionary<string, object>
            {
                { "ItemName", item.name },
                { "ItemType", item.ItemType.name }
            };

            var options = new UpdatePlayersInventoryItemOptions { WriteLock = null };
            await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync("GearSlotId_" + item.ItemType.name, instanceData, options);
        }
    }

    private async Task RemoveItemFromInventoryTask(InventoryItem item)
    {
        var options = new UpdatePlayersInventoryItemOptions { WriteLock = null };
        await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync("InventorySlotId_" + item.InventorySlotId, null, options);
    }

    private async Task RemoveItemFromGearTask(ItemSO item)
    {
        var options = new UpdatePlayersInventoryItemOptions { WriteLock = null };
        await EconomyService.Instance.PlayerInventory.UpdatePlayersInventoryItemAsync("GearSlotId_" + item.ItemType.name, null, options);
    }
 
}
