using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]

public class EnemySO : ScriptableObjectWithIcon
{

    // Atributy tøídy EnemySO
    [field: SerializeField] public Sprite Weapon { get; set; }
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public int BaseHealth { get; set; }
    [field: SerializeField] public int BaseDamage { get; set; }
    [field: SerializeField] public int BaseGoldReward { get; set; }
    [field: SerializeField] public int BaseXpRevard { get; set; }
 
    [field: SerializeField] public RaritySO Rarity { get; set; }
    public void Init(Sprite spr, RaritySO rarity)
    {
        MainSprite = spr;
        Rarity = rarity;
    }
 
}
