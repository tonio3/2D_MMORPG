using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleCard : BattleCard
{
    //UI
    [SerializeField] Sprite _handImg;

    public void Init(CharacterAttributesSO character, CharacterIdentitySO identity)
    {
        MaxHp = character.Health;
        Hp = MaxHp;
        Damage = character.Damage;
        weaponImg.sprite = Player.Instance.GetWeaponImage();
        img.sprite = identity.CharacterSprite;
        _levelTxt.text = character.Level + "";

        if (weaponImg.sprite == null) weaponImg.sprite = _handImg;
        hpSlider.value = (float)Hp / MaxHp;
        hpTxt.text = character.Health + "";
        dmgTxt.text = character.Damage + "";
    }
 

}
