using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBattleCard : BattleCard
{
  
    public void Init(EnemySO SO)
    {
     
        img.sprite = SO.MainSprite;
 
        weaponImg.sprite = SO.Weapon;
        Hp = SO.BaseHealth;
        MaxHp = Hp;
        Damage = SO.BaseDamage;
        hpSlider.value = (float)Hp / MaxHp;
        _levelTxt.text = SO.Level + "";
        hpTxt.text = Hp + "";
        dmgTxt.text = Damage + "";
    }

    public void InitAsPVP(EnemySO SO)
    {

        img.sprite = SO.MainSprite;

        weaponImg.sprite = SO.Weapon;
        Hp = SO.BaseHealth;
        MaxHp = Hp;
        Damage = SO.BaseDamage;
        hpSlider.value = (float)Hp / MaxHp;
        _levelTxt.text = SO.Level + "";
        hpTxt.text = SO.BaseHealth + "";
        dmgTxt.text = SO.BaseDamage + "";
    }


}
