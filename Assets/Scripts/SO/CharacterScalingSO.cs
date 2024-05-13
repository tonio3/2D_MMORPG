using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScaling", menuName = "ScriptableObjects/CharacterScaling")]
public class CharacterScalingSO : ScriptableObject
{
    [SerializeField] private int _xpPerLevelScaling;
    [SerializeField] private int _xpPerLevelSkillPointsReward;
    [SerializeField] private int _strengthPerPoint;
    [SerializeField] private int _endurancePerPoint;

    public int XpPerLevelScaling => _xpPerLevelScaling;

    public int StrengthPerPoint => _strengthPerPoint;
    public int EndurancePerPoint => _endurancePerPoint;
    public int XpPerLevelSkillPointsReward => _xpPerLevelSkillPointsReward;
}
