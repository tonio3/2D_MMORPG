using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class ResourceSlot
{
    [SerializeField] [HideInInspector] string name;
    [SerializeField] int ammount;
 
    [SerializeField] ResourceSO sO;
    
    public ResourceSlot( ResourceSO sO)
    {
        this.sO = sO;
        name = sO.resourceType + "_Slot";
    }

    public int Ammount { get { return ammount; } set { ammount= value; } }
    public ResourceSO Resource { get { return sO; } }
}
