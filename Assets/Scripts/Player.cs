using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Unity.Services.Core.Environments;
using UnityEngine;
using System.Linq;
using TMPro;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.Services.Leaderboards;


public class Player : MonoBehaviour
{
 
    [SerializeField] PlayerCurrencySO _currency;

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<Player>();
            return _instance;
        }

        set
        {
            Instance = value;
        }
    }
  

    public Sprite GetWeaponImage()
    {/*
        var weapon = Gear[(int)ItemType.Weapon].item;

        if (weapon == null) return null;
        return weapon.Spr;*/
        return null;
    }
 
}
