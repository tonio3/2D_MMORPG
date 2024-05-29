using UnityEngine;
using TMPro;
using System;

public class CurrencyManagerUI : UIEventSubscriber
{
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _skillPointsText;

    [SerializeField] private PlayerCurrencySO _playerCurrency;
 

    protected override void InitialUIUpdate()
    {
        UpdateGoldText(_playerCurrency.Gold);
        UpdateSkillPointsText(_playerCurrency.SkillPoints);
    }

 
    protected override void SubscribeToPlayerEvents()
    {

        _playerCurrency.OnGoldChanged += UpdateGoldText;
        _playerCurrency.OnSkillPointsChanged += UpdateSkillPointsText;
    }
 

    protected override void UnsubscribeFromPlayerEvents()
    {
        _playerCurrency.OnGoldChanged -= UpdateGoldText;
        _playerCurrency.OnSkillPointsChanged -= UpdateSkillPointsText;
    }

    private void UpdateGoldText(int gold)
    {
        _goldText.text = gold + "";
    }

    private void UpdateSkillPointsText(int skillPoints)
    {
        _skillPointsText.text =skillPoints + "";
    }
 
}
