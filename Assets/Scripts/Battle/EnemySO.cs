using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]

public class EnemySO : ScriptableObjectWithIcon
{

    // Atributy tøídy EnemySO
    [field: SerializeField] public Sprite Weapon { get; set; }
    [field: SerializeField] public int Level { get; set; } = -1;
    [field: SerializeField] public int BaseHealth { get; set; }
    [field: SerializeField] public int BaseDamage { get; set; }
    [field: SerializeField] public int BaseGoldReward { get; set; }
    [field: SerializeField] public int BaseXpRevard { get; set; }

    [SerializeField] EnemyLevelBasedStatsIncreaseSO _levelBasedStats;

    public int CalcHealthBasedOnLevel()
    {
        return BaseHealth + (_levelBasedStats.HealthPerLevel * Level);
    }

    public int CalcDamageBasedOnLevel()
    {
        return BaseDamage + (_levelBasedStats.DamagePerLevel * Level);
    }

    public int CalcGoldRewardBasedOnLevel()
    {
        if (_levelBasedStats == null) return 0;
        return BaseGoldReward + (_levelBasedStats.GoldRevardPerLevel * Level);
    }

    public int CalcXpRewardBasedOnLevel()
    {
        if (_levelBasedStats == null) return 0;
        return BaseXpRevard + (_levelBasedStats.XpRevardPerLevel * Level);
    }

}
