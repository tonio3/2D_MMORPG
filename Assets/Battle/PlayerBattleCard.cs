using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleCard : BattleCard
{
    //UI

    [SerializeField] Sprite HandImg;

    public void Init()
    {
        MaxHp = Player.Instance.GetHp();
        Hp = MaxHp;
       AttackDamage = Player.Instance.GetAttackDamage();
       weaponImg.sprite = Player.Instance.GetWeaponImage();

       if (weaponImg.sprite == null) weaponImg.sprite = HandImg;
        hpSlider.value = (float)Hp / MaxHp;

    }
 

}
