using UnityEngine;

//[CreateAssetMenu]
public class RewardsPerLevelSO : ScriptableObject
{
    [SerializeField] private int _skillPointsPerLevel;

    public int SkillPointsPerLevel => _skillPointsPerLevel; 
    
}