using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private PlayerCurrencySO  _playerCurrencySO;
    [SerializeField] private CharacterAttributesSO _playerAttributesSO;

    [SerializeField] private RewardsPerLevelSO _rewardsPerLevelSO;

    private void OnEnable()
    {
        _playerAttributesSO.OnLevelChanged += HandleLevelChanged;
    }

    private void OnDisable()
    {
        _playerAttributesSO.OnLevelChanged -= HandleLevelChanged;
    }

    private void HandleLevelChanged(int newLevel)
    {

        _playerCurrencySO.SkillPoints += _rewardsPerLevelSO.SkillPointsPerLevel;
    }
}
