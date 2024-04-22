using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]

public class EnemySO : ScriptableObject
{

    public int Hp;
    public int AttackDamage;
    public Sprite Spr;
    public Sprite Weapon;

    public int price = 0;
    public int XP = 0;


}
