using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]

public class LevelScalingEnemySO : EnemySO
{
 
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
