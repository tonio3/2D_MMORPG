using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBattleCard : BattleCard
{
    EnemySO SO; 

    public void Init(EnemySO SO)
    {
     
        img.sprite = SO.Spr;

        //hpTxt.text = "Hp " + SO.Hp;    

        weaponImg.sprite = SO.Weapon;
        Hp = SO.Hp;
        MaxHp = Hp;
        AttackDamage = SO.AttackDamage;
        hpSlider.value = (float)Hp / MaxHp;
    }

 

    public int GetPrice()
    {
        return SO.price;
    }


}
