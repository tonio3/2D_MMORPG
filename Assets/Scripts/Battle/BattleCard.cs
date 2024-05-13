using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleCard : MonoBehaviour
{
    protected int Hp;
    protected int MaxHp;

    [SerializeField] protected TextMeshProUGUI _levelTxt;
    [SerializeField] protected Image img;
    [SerializeField] protected Image weaponImg;

    [SerializeField] protected TextMeshProUGUI hpTxt;
    [SerializeField] protected TextMeshProUGUI dmgTxt;

    [SerializeField] Animator animAttack;
    [SerializeField] Animator animDamage;
    public int AttackSpeed;

    protected int Damage;

    [SerializeField] protected Slider hpSlider;

    public void Attack()
    {
        animAttack.SetTrigger("Attack");
        //anim.speed = 1000/AttackSpeed;
    }

    public int GetAttackDamage()
    {
        return Damage;
    }

    public bool CanTakeDamage(int dmg)
    {
        return Hp > dmg;
    }

    public bool IsAlive()
    {
        return Hp > 0;
    }
 

    public void TakeDamage(int dmg)
    {
        animDamage.SetTrigger("TakeDamage");
        Hp -= dmg;

        if (Hp < 0) Hp = 0;
        hpSlider.value = (float)Hp / MaxHp;
    }
}
