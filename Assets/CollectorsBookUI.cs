
using Fishing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectorsBookUI : UIEventSubscriber
{
    [SerializeField] private CollectorsBookSlotUI[] _fishSlots;
    [SerializeField] private CollectorsBookSlotUI[] _junkSlots;

    [SerializeField] private CollectiblesSO _collectorBookSO;


    protected override void InitialUIUpdate()
    {
        LoadBook();
    }

    protected override void SubscribeToPlayerEvents()
    {

    }

    protected override void UnsubscribeFromPlayerEvents()
    {
      
    }
  

    private void LoadBook()
    {
        foreach (var item in _collectorBookSO.Fish)
        {
            foreach (var slot in _fishSlots)
            {
                if (slot.Reference == null) continue;
                if (item.name == slot.Reference.name)
                {
                    slot.Obtained = true;
                }
            }
        }

        foreach (var item in _collectorBookSO.JunkItems)
        {
            foreach (var slot in _junkSlots)
            {
                if (slot.Reference == null) continue;
                if (item.name == slot.Reference.name)
                {
                    slot.Obtained = true;
                }
            }
        }
    }
}
