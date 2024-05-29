using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishingLootSO : ScriptableObjectWithIcon, IRarity
{
 
    [field: SerializeField] public RaritySO Rarity { get; set; }

    public void Init(Sprite spr, RaritySO rarity)
    {
        MainSprite = spr;
        Rarity = rarity;
    }
}
