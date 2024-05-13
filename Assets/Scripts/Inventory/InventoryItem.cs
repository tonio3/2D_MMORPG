using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem 
{
    public ItemSO Item;
    public int InventorySlotId { get; set; }
}
