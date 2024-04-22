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

[Serializable]
public class GearItem
{
 
    public string name;
    public ItemSO item;
}

public class Player : MonoBehaviour
{
 
    [SerializeField] DatabaseSO database;
   
    [SerializeField] TextMeshProUGUI CurrencyUI; 
    [SerializeField] TextMeshProUGUI SkillPointsUI;

    [SerializeField]  GearItem[] Gear;

    [SerializeField] ItemSO[] Inventory;
    public bool bookUnlocked;

    [SerializeField] RuneType RuneType;
    [SerializeField] PlayerData _pd;

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

    private void Start()
    {
        _pd = PlayerData.Instance;
        CurrencyUI.text = _pd.Gold + "";
        SkillPointsUI.text = _pd.SkillPoints + "";
    }

    private int CalculateNextLVXP()
    {
        return _pd.Level * 100;
    }

    private int GetXP()
    {
        return _pd.XP;
    }

    internal void SetRune(RuneType rune)
    {
        RuneType = rune;
    }

    public void UnlockBasicBook()
    {
        if(_pd.TreePoints >= 1)
        {
            _pd.TreePoints--;
            bookUnlocked = true;
        }
    }

    public void SetXP(int i)
    {
        _pd.XP += i;
        var x = CalculateNextLVXP();
        if (_pd.XP > x)
        {
           
            OnLevelUp();
        }
    }

    internal int GetHp()
    {
        var maxHp = _pd.Health * 100;
        return _pd.Health;
    }

    internal Sprite GetWeaponImage()
    {
        var weapon = Gear[(int)ItemType.Weapon].item;

        if (weapon == null) return null;
        return weapon.Spr;
    }


    internal int GetAttackDamage()
    {
        var damage = _pd.Strength * 100;
        return damage;
    }

    internal void SetGearItem(ItemSO item)
    {
        Gear[(int)item.Type].item = item;
    }

    internal void RemoveGearItem(ItemSO item)
    {
        Gear[(int)item.Type].item = null;
    }

  

    public bool CanBuy(int i)
    {
        if (_pd.Gold >= i) { _pd.Gold -= i; CurrencyUI.text = _pd.Gold + "";  return true; }
        return false;
    }

    public void Sell(int i)
    {
        _pd.Gold += i;
        CurrencyUI.text = _pd.Gold + "";
    }

    public bool CanUseSkillPoint()
    {
        if (_pd.SkillPoints == 0) return false;
        _pd.SkillPoints--;
        return true;
       
    }

    public void AddStrenght() //via UI button event
    {  
        if(!CanUseSkillPoint()) return;
       _pd.Strength++;     
    }

    public void AddIntellignece() //via UI button event
    {
        if (!CanUseSkillPoint()) return;
        _pd.Intelligence++;
    }

    public void AddDexterity() //via UI button event
    {
        if (!CanUseSkillPoint()) return;
        _pd.Dexterity++;
    }

    public void AddLuck() //via UI button event
    {
        if (!CanUseSkillPoint()) return;
        _pd.Luck++;
    }

    public void AddHealth() //via UI button event
    {
        if (!CanUseSkillPoint()) return;
        _pd.Health++;
    }
 

    private void OnLevelUp()
    {
        _pd.XP = 0;

        _pd.Level++;

        if (_pd.Level % 10 == 0)
            AddTreePoint(1);
        AddSkillPoint(1);       
    }


    private void AddSkillPoint(int i)
    {
        _pd.SkillPoints += i;
    }

    private void AddTreePoint(int i)
    {
        _pd.TreePoints += i;
    }


}
