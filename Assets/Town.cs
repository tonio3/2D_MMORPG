using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public  class Town
{
    [SerializeField] [HideInInspector]string name;
    public TownE town;
    public TownResource TownRes;
    
    public void OnValidate()
    {
        name = town.ToString();
    }
   
}