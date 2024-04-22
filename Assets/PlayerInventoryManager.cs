using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] List<ResourceSlot> resourceSlotsCloth = new List<ResourceSlot>();
    [SerializeField] List<ResourceSlot> resourceSlotsIngots = new List<ResourceSlot>();
    [SerializeField] List<ResourceSlot> resourceSlotsLeather = new List<ResourceSlot>();
    [SerializeField] List<ResourceSlot> resourceSlotsHerbs = new List<ResourceSlot>();

    public void InsertNewItem(ItemSO item)
    {
        ItemSlot slot;

        slot = GetFreeSlot();

        if(slot == null) { return; }

        slot.SetItem(item);
    }

    public void InsertResources(TownE city)
    {
        Town a = DatabaseSO.dSO.countries.FirstOrDefault(slot => slot.town == city);

        var i = Random.Range(1, 5);
        var resources = new ResourceSO[i];
        var slots = new ResourceSlot[i];

        for (int j = 0; j < i; j++)
        {
            resources[j] = DatabaseSO.dSO.GetRandomResource(a.TownRes.op);

            List<ResourceSlot> resourceSlots = resourceSlotsHerbs; 

            switch (resources[j].resourceType)
            {
            case ResourceType.Herb:
                    resourceSlots = resourceSlotsHerbs;
                break;
            case ResourceType.Leather:
                    resourceSlots = resourceSlotsLeather;
                    break;
            case ResourceType.Cloth:
                    resourceSlots = resourceSlotsCloth;
                    break;
            case ResourceType.Ingot:
                    resourceSlots = resourceSlotsIngots;
                    break;
            default:
                break;
            }

        slots[j] = resourceSlots.FirstOrDefault(slot => slot.Resource.name == resources[j].name);

            if (slots[j] == null)
            {
                slots[j] = new ResourceSlot(resources[j]); resourceSlots.Add(slots[j]);
            }

            slots[j].Ammount++;
        }     
    }
 


    private ItemSlot GetFreeSlot()
    {
        var slots = itemSlots;

        foreach(ItemSlot slot in slots)
        {
            if (slot.Item == null) return slot;
        }

        return null;
    }

   
}
